using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartZone : MonoBehaviour {

    public GameManager gmScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !this.gmScript.GetWinState())
            this.gmScript.ResetGame();
    }
}
