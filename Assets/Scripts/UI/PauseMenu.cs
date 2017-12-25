using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public PlayerUI uiScript;
    public string homeScene;

    public void Resume()
    {
        uiScript.UnpauseScreen();
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
