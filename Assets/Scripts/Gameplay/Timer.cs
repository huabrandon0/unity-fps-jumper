using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float startTime = 0f;
    private float runningTime = 0f;
    private bool pause = false;
    private float pauseStartTime = 0f;

    public Text timeText;

	void Start () {
        ResetTime();
	}
	
    void Update () {
        if (this.pause)
            return;
        
        this.runningTime = Time.time - this.startTime;
        UpdateText(this.runningTime);
	}

    public float GetRunningTime()
    {
        return this.runningTime;
    }

    public void ResetTime()
    {
        this.startTime = Time.time;
        this.pauseStartTime = Time.time;

        this.runningTime = 0f;
        UpdateText(0f);
    }

    public void PauseTime()
    {
        if (this.pause)
            return;

        this.pause = true;
        this.pauseStartTime = Time.time;
    }

    public void StartTime()
    {
        if (!this.pause)
            return;

        this.pause = false;

        // Increase startTime to account for the time spent paused
        this.startTime += Time.time - this.pauseStartTime;
    }

    private void UpdateText(float time)
    {
        System.TimeSpan ts = System.TimeSpan.FromSeconds(time);
        timeText.text = string.Format("{0:00}:{1:00}.{2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
    }
}
