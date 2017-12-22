using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float startTime = 0f;
    private float runningTime = 0f;
    private bool stop = false;

    public Text timeText;

	// Use this for initialization
	void Start () {
        ResetTime();
	}
	
	// Update is called once per frame
    void Update () {
        if (this.stop)
            return;
        
        this.runningTime = Time.time - this.startTime;
        System.TimeSpan t = System.TimeSpan.FromSeconds(this.runningTime);
        timeText.text = string.Format("{0:00}:{1:00}.{2:000}", t.Minutes, t.Seconds, t.Milliseconds);
	}

    public void ResetTime()
    {
        this.startTime = Time.time;
        this.stop = false;
    }

    public void StopTime()
    {
        this.stop = true;
    }
}
