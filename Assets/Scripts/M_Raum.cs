using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Raum : MonoBehaviour
{
    public GameObject crows;
    public GameObject finalCrow;
    public GameObject gameDirector;
    public AudioSource killRaumSound;
    public GameObject raumAttack;

    int crowNumber = 0;
    int TOTAL_CROWS = 6;
    bool isKilling = false;

    public void Raum()
    {
        StartHaunting();
    }

    void StartHaunting()
    {
        crowNumber = 0;
        Invoke("SummonCrow", 6f + Random.Range(0f, 4.5f));
    }

    void SummonCrow()
    {
        crows.transform.GetChild(crowNumber).gameObject.SetActive(true);
        crowNumber+=1;
        if (crowNumber < TOTAL_CROWS)
        {
            Invoke("SummonCrow", 6f + Random.Range(0f, 4.5f));
            return;
        }
        Invoke("SummonFinalCrow", 10f + Random.Range(0f, 1.5f));
    }

    void SummonFinalCrow()
    {
        finalCrow.SetActive(true);
        isKilling = true;
        Invoke("KillRaum", 6f);
    }

    void KillRaum()
    {
        if (!isKilling)
        {
            return;
        }
        if (gameDirector.GetComponent<GameDirector>().PlayAudio(3f))
        {
            killRaumSound.Play();
        }
        StartCoroutine("AttackRaum");
        gameDirector.GetComponent<GameDirector>().KIA(3f);
    }

    IEnumerator AttackRaum()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            raumAttack.transform.localScale += new Vector3(-2*Time.deltaTime, 2*Time.deltaTime, 0f);
            raumAttack.SetActive(true);
            yield return null;
        }
    }
}
