using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGuide : MonoBehaviour
{
    public int currentWard = 0;
    public GameObject map;
    public GameObject areas;

    bool isMapActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isMapActive = !isMapActive;
            map.SetActive(isMapActive);
            areas.SetActive(isMapActive);
        }
    }

    public void SetWardActive(int ix)
    {
        areas.transform.GetChild(currentWard).gameObject.SetActive(false);
        currentWard = ix;
        areas.transform.GetChild(currentWard).gameObject.SetActive(true);
    }
}
