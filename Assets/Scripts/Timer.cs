using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    static float time = 0f;
    bool countTime = false;

    // Update is called once per frame
    void Update()
    {
        if (countTime)
        {
            time += Time.deltaTime;
        }
    }

    public void IsTimerOn(bool isOn)
    {
        countTime = isOn;
    }

    public void resetTimer()
    {
        time = 0f;
    }

    public float GiveTime()
    {
        return time;
    }

    public string GiveStringTime()
    {
        float score = time;
        int hours = (int)(score/3600);
        int minutes = (int)((score - hours*3600)/60);
        int seconds = (int)(score - hours*3600 - minutes * 60);
        string playerTime = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        return playerTime;
    }

    public string GiveStringTimeShort()
    {
        float score = time;
        int minutes = (int)(score/60);
        int seconds = (int)(score - minutes * 60);
        int msecs = (int)((score - (int)score)*100);
        string playerTime = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + msecs.ToString("00");
        return playerTime;
    }
}