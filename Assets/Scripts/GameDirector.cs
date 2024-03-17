using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public static float difficultyLevel = 10f;
    public Text timerText;
    public GameObject nocturne;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<M_Evelynn>().Evelynn();
        GetComponent<M_Fiddlesticks>().Fiddlesticks();
        GetComponent<Timer>().resetTimer();
        GetComponent<Timer>().IsTimerOn(true);
        nocturne.transform.GetComponent<M_Nocturne>().Nocturne();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = GetComponent<Timer>().GiveStringTimeShort();
    }


}