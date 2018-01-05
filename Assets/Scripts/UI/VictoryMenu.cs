using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {

    public string homeScene;

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
