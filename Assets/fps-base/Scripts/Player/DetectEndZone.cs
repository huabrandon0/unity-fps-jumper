using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEndZone : MonoBehaviour {

    public Timer tmrScript;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "EndZone")
        {
            Debug.Log("WIN");
            this.tmrScript.StopTime();
        }
    }
}
