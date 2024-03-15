using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class M_Evelynn : MonoBehaviour
{
    
    public GameObject evePos;
    public AudioSource eveAppear;
    public GameObject eveLaughs;
    public AudioSource eveKill;
    public GameObject eveAttack;


    public void Evelynn()
    {
        InvokeRepeating("Evelynn_appear", 10f, 10f);   // to scale
    }

    void Evelynn_appear()
    {
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;

        float minDist = 9999f;
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
        if (ix >= 0)
        {
            evePos.transform.GetChild(ix).gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 0));
            evePos.transform.GetChild(ix).gameObject.SetActive(true);
            eveAppear.Play();
            Invoke("PauseEveAudio", 2.5f);
            StartCoroutine(FadeInEve(ix));
            
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
                int lix = Random.Range(0, eveLaughs.transform.childCount);
                eveLaughs.transform.GetChild(lix).GetComponent<AudioSource>().Play();
                Invoke("EveKill", 3f);
                evePos.transform.GetChild(j).gameObject.SetActive(false);
                yield break;
            }

            yield return null;
        }
        evePos.transform.GetChild(j).gameObject.SetActive(false);
    }

    void PauseEveAudio()
    {
        eveAppear.Stop();
        eveKill.Stop();
    }

    void EveKill()
    {
        eveKill.Play();
        Invoke("PauseEveAudio", 1.2f);
        StartCoroutine("EveAttack");
        Invoke("KIA", 1f);
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

    void KIA()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
