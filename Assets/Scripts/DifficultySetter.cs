using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    public static bool isEvelynn = true;
    public static bool isFiddle = true;
    public static bool isNocturne = true;
    public static bool isRaum = true;
    public static bool isTahm = true;

    public void setEvelynn(bool k)
    {
        isEvelynn = k;
    }

    public void setFiddlesticks(bool k)
    {
        isFiddle = k;
    }

    public void setNocturne(bool k)
    {
        isNocturne = k;
    }

    public void setRaum(bool k)
    {
        isRaum = k;
    }

    public void setTahm(bool k)
    {
        isTahm = k;
    }

    public bool getEvelynn()
    {
        return isEvelynn;
    }

    public bool getFiddle()
    {
        return isFiddle;
    }

    public bool getNocturne()
    {
        return isNocturne;
    }

    public bool getRaum()
    {
        return isRaum;
    }

    public bool getTahm()
    {
        return isTahm;
    }
}
