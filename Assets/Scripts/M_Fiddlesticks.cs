using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Fiddlesticks : MonoBehaviour
{
    public GameObject gameDirector;
    public GameObject fiddlesticks;

    // Start is called before the first frame update
    public void Fiddlesticks()
    {
        Invoke("StartSpawning", 2f);
    }

    void StartSpawning()
    {
        int k = Random.Range(0, 20);
        if (k < 1)
        {
            Spawn();
        }
        Invoke("StartSpawning", 0.5f);
    }

    void Spawn()
    {
        //which surr
        int surr = Random.Range(1, gameDirector.GetComponent<PlayerControls>().GetTotalSurr());
        surr = (surr + gameDirector.GetComponent<PlayerControls>().GetCurrSurr()) % gameDirector.GetComponent<PlayerControls>().GetTotalSurr();

        //which fiddle
        int fiddIx = Random.Range(0, fiddlesticks.transform.childCount);

        GameObject newFiddle = Instantiate(fiddlesticks.transform.GetChild(fiddIx).gameObject);
        newFiddle.transform.position = new Vector3(Random.Range(-7.75f, 7.75f), Random.Range(-4f, 4f), 0f);
        newFiddle.transform.parent = gameDirector.GetComponent<PlayerControls>().surroundings.transform.GetChild(surr);
        newFiddle.SetActive(true);

        GameObject effigyHolder = new GameObject();
        newFiddle.GetComponent<M_FiddleEffigy>().effigyHolder = effigyHolder;
        effigyHolder.AddComponent<M_Effigy>();
        effigyHolder.GetComponent<M_Effigy>().fiddleParent = newFiddle;
        effigyHolder.GetComponent<M_Effigy>().fiddleAttack = newFiddle.GetComponent<M_FiddleEffigy>().fiddleAttack;
        effigyHolder.GetComponent<M_Effigy>().fiddleKill = newFiddle.GetComponent<M_FiddleEffigy>().fiddleKill;
        effigyHolder.GetComponent<M_Effigy>().fiddleLaughs = newFiddle.GetComponent<M_FiddleEffigy>().fiddleLaughs;
    }
}
