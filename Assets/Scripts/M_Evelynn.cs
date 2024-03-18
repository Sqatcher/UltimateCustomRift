using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class M_Evelynn : MonoBehaviour
{
    
    public GameObject evePos;
    public AudioSource eveAppear;
    public GameObject eveLaughs;
    public AudioSource eveKill;
    public GameObject eveAttack;
    public GameObject gameDirector;

    float EVELYNN_APPEAR_S = 10f;
    bool isAlluring = false;
    float allureTime = 0;
    int activeEve = -1;

    public void Evelynn()
    {
        Invoke("Evelynn_appear", EVELYNN_APPEAR_S + Random.Range(0f, 5f));
    }

    void Evelynn_appear()
    {
        Invoke("Evelynn_appear", EVELYNN_APPEAR_S + Random.Range(0f, 5f));
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;

        float minDist = 9999999f;
        int ix = -1;
        for (int i=0; i<evePos.transform.childCount; i++)
        {
            float dist = Vector3.Distance(mousePosition, evePos.transform.GetChild(i).position);
            if (dist < minDist)
            {
                minDist = dist;
                ix=i;
            }
        }
        activeEve = ix;
        if (activeEve >= 0)
        {
            isAlluring = true;
            evePos.transform.GetChild(activeEve).gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 0));
            evePos.transform.GetChild(activeEve).gameObject.SetActive(true);
            eveAppear.Play();
            Invoke("PauseEveAudio", 2.5f);
            StartCoroutine(FadeInEve(activeEve));
            
        }
    }

    void Update()
    {
        if (isAlluring)
        {
            allureTime += Time.deltaTime;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0f;
            float dist = Vector3.Distance(mousePosition, evePos.transform.GetChild(activeEve).position);

            if (dist < 200f && Input.GetKeyDown(KeyCode.Mouse0) && allureTime >= 0.5f)
            {
                isAlluring = false;
                allureTime = 0;
                int lix = Random.Range(0, eveLaughs.transform.childCount);
                eveLaughs.transform.GetChild(lix).GetComponent<AudioSource>().Play();
                Invoke("EveKill", 3f);
                evePos.transform.GetChild(activeEve).gameObject.SetActive(false);
                StopCoroutine("FadeInEve");
                return;
            }

            Vector3 change = evePos.transform.GetChild(activeEve).position - mousePosition;
            float power = 3f;
            change *= power/dist;
            // move mouse
        }
    }

    IEnumerator FadeInEve(int j)
	{
        float k = 0f;
        for (float i = 0; i <= 5; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0f;
            float dist = Vector3.Distance(mousePosition, evePos.transform.GetChild(j).position);
            //Debug.Log(dist);
            if (dist > 300f)
            {
                k += 1.5f*Time.deltaTime;
            }
            evePos.transform.GetChild(j).gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, (i-k+1f)/2.5f));
            
            if (i-k >= 1.5)
            {
                isAlluring = false;
                allureTime = 0;
                int lix = Random.Range(0, eveLaughs.transform.childCount);
                eveLaughs.transform.GetChild(lix).GetComponent<AudioSource>().Play();
                Invoke("EveKill", 3f);
                evePos.transform.GetChild(j).gameObject.SetActive(false);
                yield break;
            }

            yield return null;
        }
        evePos.transform.GetChild(j).gameObject.SetActive(false);
        isAlluring = false;
        allureTime = 0;
    }

    void PauseEveAudio()
    {
        eveAppear.Stop();
        eveKill.Stop();
    }

    void EveKill()
    {
        if (gameDirector.GetComponent<GameDirector>().PlayAudio(1.2f))
        {
            eveKill.Play();
        }
        Invoke("PauseEveAudio", 1.2f);
        StartCoroutine("EveAttack");
        gameDirector.GetComponent<GameDirector>().KIA(1.1f);
    }

    IEnumerator EveAttack()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            eveAttack.transform.localScale += new Vector3(2*Time.deltaTime, 2*Time.deltaTime, 0f);
            eveAttack.SetActive(true);
            yield return null;
        }
    }
}
