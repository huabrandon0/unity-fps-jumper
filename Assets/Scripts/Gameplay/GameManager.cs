using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform spawnPoint;
    public Timer tmrScript;
    public PlayerUI uiScript;
    public GameObject player;

    private bool hasWon = false;

    void Update()
    {
        if (InputManager.GetKeyDown("Reset"))
            ResetGame();
    }

    public void WinGame()
    {
        if (this.hasWon)
            return;
        
        this.hasWon = true;
        this.uiScript.VictoryScreen("YOUR TIME: " + tmrScript.timeText.text);
    }

    public void ResetGame()
    {
        player.transform.position = spawnPoint.position;
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
            playerScript.ResetState();

        tmrScript.ResetTime();
        tmrScript.PauseTime();

        if (!this.hasWon)
            return;
        
        this.hasWon = false;
        this.uiScript.UnVictoryScreen();
    }

    public bool GetWinState()
    {
        return this.hasWon;
    }
}
