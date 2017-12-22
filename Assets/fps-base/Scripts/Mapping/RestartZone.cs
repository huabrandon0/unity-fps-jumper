using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartZone : MonoBehaviour {

    public Transform spawnPoint;
    public Timer tmrScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = spawnPoint.position;

            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.ResetState();
                tmrScript.ResetTime();
            }
        }
    }
}
