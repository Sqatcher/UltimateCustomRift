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
    public GameObject tahm;

    bool audioPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<DifficultySetter>().getEvelynn()) evelynn.transform.GetComponent<M_Evelynn>().Evelynn();
        if (GetComponent<DifficultySetter>().getFiddle()) GetComponent<M_Fiddlesticks>().Fiddlesticks();
        if (GetComponent<DifficultySetter>().getNocturne()) nocturne.transform.GetComponent<M_Nocturne>().Nocturne();
        if (GetComponent<DifficultySetter>().getRaum()) raum.transform.GetComponent<M_Raum>().Raum();
        if (GetComponent<DifficultySetter>().getTahm()) tahm.transform.GetComponent<M_TahmKench>().StartTahm();

        GetComponent<Timer>().resetTimer();
        GetComponent<Timer>().IsTimerOn(true);
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
        GetComponent<Timer>().IsTimerOn(false);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
