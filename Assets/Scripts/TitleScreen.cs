using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Destroy(GameObject.FindGameObjectWithTag("Music"));
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}