using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnokacFade : MonoBehaviour
{
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        img.color = new Color(1, 1, 1, 0);
        FadeInFunction();
        Invoke("FadeOutFunction", 3);
        Invoke("NextScene", 5);    
    }

    void NextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void FadeOutFunction()
	{
        StartCoroutine(FadeOut());
    }

    void FadeInFunction()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
	{
        for (float i = 0; i <= 2; i += Time.deltaTime)   // i <= 1  -- over 1 second
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator FadeOut()
	{
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
