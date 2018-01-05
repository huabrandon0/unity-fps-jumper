using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Transform spawnPoint;
    public Timer tmrScript;
    public PlayerUI uiScript;
    public GameObject player;
    public ScoreManager smScript;

    private bool hasWon = false;

    void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;
        else if (GameManager.instance != this)
            Destroy(this.gameObject);
    }

    public void WinGame()
    {
        if (this.hasWon)
            return;
        
        this.hasWon = true;

        float time = tmrScript.GetRunningTime();

        if (smScript.AddScore(time, 0))
            smScript.SaveScores();
        
        this.uiScript.VictoryScreen(time, smScript.scores.ScoresList[0]);
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
