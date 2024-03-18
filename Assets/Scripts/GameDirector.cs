using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static float difficultyLevel = 10f;
    public Text timerText;
    public GameObject evelynn;
    public GameObject raum;
    public GameObject nocturne;

    bool audioPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        //evelynn.transform.GetComponent<M_Evelynn>().Evelynn();
        //GetComponent<M_Fiddlesticks>().Fiddlesticks();
        GetComponent<Timer>().resetTimer();
        GetComponent<Timer>().IsTimerOn(true);
        //nocturne.transform.GetComponent<M_Nocturne>().Nocturne();
        raum.transform.GetComponent<M_Raum>().Raum();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = GetComponent<Timer>().GiveStringTimeShort();
    }

    public bool PlayAudio(float time)
    {
        if (audioPlaying)
        {
            return false;
        }
        audioPlaying = true;
        Invoke("AudioOff", time);
        return true;
    }

    void AudioOff()
    {
        audioPlaying = false;
    }

    public void KIA(float time)
    {
        Invoke("KIAd", time);
    }

    void KIAd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
