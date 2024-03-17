using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_Effigy : MonoBehaviour
{
    public AudioSource fiddleKill;
    public GameObject fiddleLaughs;
    public GameObject fiddleAttack;
    public GameObject fiddleParent;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FiddleLaugh", 8f);
        Invoke("FiddleKill", 10f);
    }

    void FiddleLaugh()
    {
        int ix = Random.Range(0, fiddleLaughs.transform.childCount);
        fiddleLaughs.transform.GetChild(ix).GetComponent<AudioSource>().Play();
    }

    void FiddleKill()
    {
        fiddleParent.GetComponent<M_FiddleEffigy>().immortal = true;
        fiddleKill.Play();
        StartCoroutine("FiddleAttack");
        Invoke("KIA", 3.5f);
    }

    IEnumerator FiddleAttack()
    {
        for (float i = 0; i <= 3; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            if (i >= 2)
            {
                fiddleAttack.transform.localScale += new Vector3(2*Time.deltaTime, 2*Time.deltaTime, 0f);
                fiddleAttack.SetActive(true);
            }
            yield return null;
        }
    }

    void KIA()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}