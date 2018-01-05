using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZones : MonoBehaviour {
    
    public Timer tmrScript;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "EndZone" && !GameManager.instance.GetWinState())
        {
            this.tmrScript.PauseTime();
            GameManager.instance.WinGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StartZone")
        {
            this.tmrScript.ResetTime();
            this.tmrScript.PauseTime();

            if (GameManager.instance.GetWinState())
                GameManager.instance.ResetGame(); // may not be necessary
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "StartZone")
        {
            this.tmrScript.StartTime();
        }
    }
}
