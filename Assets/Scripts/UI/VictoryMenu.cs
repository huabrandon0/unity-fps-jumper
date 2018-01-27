using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class VictoryMenu : MonoBehaviour {

	[SerializeField] private string homeScene;
	[SerializeField] private string scenePrefix;

	public void LoadNextLevel()
	{
		Regex r = new Regex(this.scenePrefix + @"(\d+)");
		Match m = r.Match(SceneManager.GetActiveScene().name);
		if (m.Success)
		{
			int level = int.Parse(m.Groups[1].Value) + 1;
			SceneManager.LoadScene(this.scenePrefix + level);
		}
		else
			Debug.Log("Error: active scene name does not correspond with scene prefix.");
	}

    public void ResetGame()
    {
        GameManager.instance.ResetGame();
    }

    public void LoadHomeScreen()
    {
        SceneManager.LoadScene(homeScene);
    }

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}
