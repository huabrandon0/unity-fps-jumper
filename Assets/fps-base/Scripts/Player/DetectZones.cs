using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZones : MonoBehaviour {

    public GameManager gmScript;
    public Timer tmrScript;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "EndZone" && !this.gmScript.GetWinState())
        {
            this.tmrScript.PauseTime();
            this.gmScript.WinGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StartZone")
        {
            this.tmrScript.ResetTime();
            this.tmrScript.PauseTime();

            if (this.gmScript.GetWinState())
                this.gmScript.ResetGame(); // may not be necessary
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
