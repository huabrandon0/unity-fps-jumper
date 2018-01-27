using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Transform spawnPoint;
    public Timer tmrScript;
    public PlayerUI uiScript;
    public GameObject player;
	public string scenePrefix;

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

		Regex r = new Regex(this.scenePrefix + @"(\d+)");
		Match m = r.Match(SceneManager.GetActiveScene().name);
		if (m.Success)
		{
			int levelIndex = int.Parse(m.Groups[1].Value) - 1;
			if (ScoreManager.instance.AddScore(time, levelIndex))
				ScoreManager.instance.SaveScores();
			
			this.uiScript.VictoryScreen(time, ScoreManager.instance.scores.ScoresList[levelIndex]);
		}
		else
			Debug.Log("Error: active scene name does not correspond with scene prefix.");
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
