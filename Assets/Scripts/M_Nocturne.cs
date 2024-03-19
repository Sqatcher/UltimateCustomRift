using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Nocturne : MonoBehaviour
{
    public GameObject areas;
    public GameObject darkness;
    public GameObject gameDirector;
    public GameObject trueDarkness;
    public GameObject nocturneLaughs;

    int hauntingArea = -1;
    bool isHaunting = false;
    float hauntingTime = 0;
    int nAreas;

    float NOCTURNE_HAUNTING_NEEDED = 20f;

    // Start is called before the first frame update
    void Start()
    {
        nAreas = areas.transform.childCount - 1;
    }

    void Update()
    {
        if (isHaunting)
        {
            hauntingTime += Time.deltaTime;
            darkness.GetComponent<CanvasRenderer>().SetAlpha(hauntingTime / (NOCTURNE_HAUNTING_NEEDED/2));
        }

        if (isHaunting && hauntingTime >= NOCTURNE_HAUNTING_NEEDED)
        {
            darkness.transform.SetParent(transform);
            isHaunting = false;
            Haunting();
        }
    }

    public void Nocturne()
    {
        Invoke("StartHaunting", 4f);
    }

    public bool resetNocturne()
    {
        if (!areas.transform.GetChild(hauntingArea).gameObject.activeInHierarchy)
        {
            return false;
        }
        isHaunting = false;
        hauntingTime = 0;
        darkness.SetActive(false);
        Invoke("StartHaunting", 5f + Random.Range(0f, 12f));
        return true;
    }

    void StartHaunting()
    {
        hauntingArea = Random.Range(0, nAreas);
        darkness.transform.SetParent(areas.transform.GetChild(hauntingArea));
        isHaunting = true;
        hauntingTime = 0;
        darkness.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 0));
        darkness.SetActive(true);
    }

    void Haunting()
    {
        isHaunting = false;
        darkness.SetActive(false);
        trueDarkness.SetActive(true);
        trueDarkness.transform.SetParent(areas.transform);
        trueDarkness.transform.SetSiblingIndex(nAreas);
        nocturneLaughs.transform.GetChild(Random.Range(0, nocturneLaughs.transform.childCount)).GetComponent<AudioSource>().Play();
        Invoke("StopHaunting", 5f);
        Invoke("StartHaunting", 8f);
    }

    void StopHaunting()
    {
        trueDarkness.SetActive(false);
        trueDarkness.transform.SetParent(transform);
    }
}
