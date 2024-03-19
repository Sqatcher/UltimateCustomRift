using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public AudioSource gameOverMusic;
    public AudioSource titleMusic;

    float SCREEN_TIME = 10f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMusic.time = 106.4f;
        gameOverMusic.Play();
        gameOverMusic.volume = 0.0f;
        StartCoroutine(FadeInAudio());
        StartCoroutine(FadeOutAudio());
        Invoke("ReturnToMenu", SCREEN_TIME);

        StartCoroutine(FadeInScore());

        GameObject gameDirector = GameObject.FindGameObjectWithTag("GameDirector");
        scoreText.text = gameDirector.GetComponent<Timer>().GiveStringTimeShort();
        Destroy(gameDirector);
    }

    IEnumerator FadeInAudio()
    {
        for (float i = 0; i <= 2; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            gameOverMusic.volume += Time.deltaTime/2f;
            yield return null;
        }
    }

    IEnumerator FadeOutAudio()
    {
        for (float i = 0; i <= SCREEN_TIME; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            if (i > SCREEN_TIME - 2)
            {
                gameOverMusic.volume -= Time.deltaTime/2f;
            }
            yield return null;
        }
    }

    IEnumerator FadeInScore()
    {
        for (float i = 0; i <= 5; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            scoreText.GetComponent<CanvasRenderer>().SetAlpha(i/5f);
            yield return null;
        }
    }

    void ReturnToMenu()
    {
        DontDestroyOnLoad(titleMusic);
        gameOverMusic.Stop();
        titleMusic.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
