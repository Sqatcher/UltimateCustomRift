using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TahmKench : MonoBehaviour
{
    public GameObject tahmFamily;
    public GameObject tahmImage;
    public GameObject tahmLaughs;
    public GameObject feedBar;
    public GameObject feedImage;
    public AudioSource feedingSound;
    public GameObject tahmAttack;
    public GameObject gameDirector;
    public GameObject tahmKillSounds;
    public AudioSource tahmMusic;

    bool isTahmActive = false;
    bool isSceneActive = false;
    bool isTahmAttacking = false;
    bool mouseHeld = false;
    bool isTahmInTheGame = false;

    float hunger;
    float HUNGER_MAX = 50f;

    // Start is called before the first frame update
    void Start()
    {
        hunger = HUNGER_MAX;
    }

    public void StartTahm()
    {
        isTahmInTheGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTahmInTheGame)
        {
            return;
        }

        if (isTahmAttacking)
        {
            return;
        }

        hunger -= Time.deltaTime;

        if (!isSceneActive && tahmFamily.activeInHierarchy)
        {
            MaybeSummonTahm();
        }

        isSceneActive = tahmFamily.activeInHierarchy;

        if (!isSceneActive)
        {
            mouseHeld = false;
            if (isTahmActive)
            {
                TahmHungry();
            }
            return;
        }

        UpdateHunger();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0f;
            float dist = Vector3.Distance(mousePosition, tahmImage.transform.position);
            if (dist < 300)
            {
                if (isTahmActive)
                {
                    isTahmActive = false;
                    tahmImage.SetActive(false);
                }
                else
                {
                    TahmHungry();
                }
            }

            dist = Vector3.Distance(mousePosition, feedImage.transform.position);
            if (dist < 70)
            {
                PressFeedingButton();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mouseHeld = false;
            feedingSound.Stop();
        }

        if (mouseHeld)
        {
            hunger += 20*Time.deltaTime;
        }
    }

    void TahmHungry()
    {
        isTahmActive = false;
        tahmImage.GetComponent<CanvasRenderer>().SetAlpha(0);
        tahmImage.SetActive(false);
        tahmLaughs.transform.GetChild(Random.Range(0, tahmLaughs.transform.childCount)).GetComponent<AudioSource>().Play();
        hunger -= 20f;
    }

    void MaybeSummonTahm()
    {
        int k = Random.Range(0, 10);
        if (k < 3)
        {
            isTahmActive = true;
            StartCoroutine("FadeInTahm");    
        }
    }

    IEnumerator FadeInTahm()
    {
        for (float i=0; i<=5; i+=Time.deltaTime)
        {
            if (isTahmActive == false)
            {
                yield break;
            }
            tahmImage.GetComponent<CanvasRenderer>().SetColor(new Color(1,1,1,i/5f));
            tahmImage.SetActive(true);
            yield return null;
        }
    }

    void UpdateHunger()
    {
        if (hunger <= 0)
        {
            isTahmAttacking = true;
            hunger = 0;
            feedBar.SetActive(false);
            Invoke("TahmKill", 3f + Random.Range(0, 10f));
            return;
        }
        if (hunger > HUNGER_MAX)
        {
            hunger = HUNGER_MAX;
        }
        Vector3 locScale = feedBar.transform.localScale;
        locScale.x = hunger/HUNGER_MAX;
        feedBar.transform.localScale = locScale;
    }

    public void PressFeedingButton()
    {
        mouseHeld = true;
        feedingSound.Play();
    }

    void TahmKill()
    {
        if (gameDirector.GetComponent<GameDirector>().PlayAudio(3.1f))
        {
            tahmKillSounds.transform.GetChild(Random.Range(0, tahmKillSounds.transform.childCount)).GetComponent<AudioSource>().Play();
            tahmMusic.Play();
        }
        StartCoroutine("TahmAttack");
        gameDirector.GetComponent<GameDirector>().KIA(3.1f);
    }

    IEnumerator TahmAttack()
    {
        for (float i = 0; i <= 3; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            if (i >= 2)
            {
                tahmAttack.transform.localScale += new Vector3(2*Time.deltaTime, 2*Time.deltaTime, 0f);
                tahmAttack.SetActive(true);
            }
            yield return null;
        }
    }
}
