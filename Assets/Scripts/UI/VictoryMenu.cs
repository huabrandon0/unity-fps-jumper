using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {

    public GameManager gmScript;
    public string homeScene;

    public void ResetGame()
    {
        this.gmScript.ResetGame();
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
