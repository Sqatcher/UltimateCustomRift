using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnokacFade : MonoBehaviour
{
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        img.color = new Color(1, 1, 1, 0);
        FadeInFunction();
        Invoke("FadeOutFunction", 5f);
        Invoke("NextScene", 9f);
    }

    void NextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void FadeOutFunction()
	{
        StartCoroutine(FadeOut());
    }

    void FadeInFunction()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
	{
        for (float i = 0; i <= 3; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i/3f);
            yield return null;
        }
    }

    IEnumerator FadeOut()
	{
        for (float i = 4; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i/4f);
            yield return null;
        }
    }
}
