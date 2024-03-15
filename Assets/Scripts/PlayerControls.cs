using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject surroundings;

    int totalSurr;
    int currSurr = 0;
    bool inTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        totalSurr = surroundings.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !inTransition)
        {
            changeSurr(1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) && !inTransition)
        {
            changeSurr(-1);
            return;
        }
        
    }

    void changeSurr(int k)
    {
        inTransition = true;
        //surroundings.transform.GetChild(currSurr).gameObject.SetActive(false);
        StartCoroutine(FadeOutSurr(currSurr));
        currSurr += k + totalSurr;
        currSurr %= totalSurr;
        //surroundings.transform.GetChild(currSurr).gameObject.SetActive(true);
        Invoke("FadeInSurrCor", 0.5f);
        Invoke("EndTransition", 1f);
    }

    IEnumerator FadeOutSurr(int k)
    {
        for (float i=0; i<=0.5; i+=Time.deltaTime)
        {
            surroundings.transform.GetChild(k).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (0.5f-i)/0.5f);
            for (int j=0; j<surroundings.transform.GetChild(k).childCount; j++)
            {
                surroundings.transform.GetChild(k).GetChild(j).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (0.5f-i)/0.5f);
            }
            yield return null;
        }
        surroundings.transform.GetChild(k).gameObject.SetActive(false);
    }

    void FadeInSurrCor()
    {
        StartCoroutine(FadeInSurr());
    }

    IEnumerator FadeInSurr()
    {
        for (float i=0; i<=0.5; i+=Time.deltaTime)
        {
            surroundings.transform.GetChild(currSurr).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i/0.5f);
            for (int j=0; j<surroundings.transform.GetChild(currSurr).childCount; j++)
            {
                surroundings.transform.GetChild(currSurr).GetChild(j).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i/0.5f);
            }
            surroundings.transform.GetChild(currSurr).gameObject.SetActive(true);
            yield return null;
        }
    }

    public int GetTotalSurr()
    {
        return totalSurr;
    }

    public int GetCurrSurr()
    {
        return currSurr;
    }

    void EndTransition()
    {
        inTransition = false;
    }
}
