using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartZone : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !GameManager.instance.GetWinState())
            GameManager.instance.ResetGame();
    }
}
