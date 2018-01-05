using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] private string playScene;

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}
