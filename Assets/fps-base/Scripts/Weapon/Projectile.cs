using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float explosionRadius = 4f;
    public float explosionForce = 200f;
    public float pushbackForce = 15f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
            return;
        
        Destroy(this.gameObject);

        var hitColliders = Physics.OverlapSphere(this.transform.position, this.explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            if (hit.tag == "Projectile")
                continue;

            Vector3 disp = hit.transform.position - this.transform.position;
            float forceScale = Mathf.Clamp((disp.magnitude != 0) ? Mathf.Cos(Mathf.PI * disp.magnitude / (1.5f * explosionRadius)) : 0, 0, 1);

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Debug.Log("colliding with " + hit.name + ": " + disp.normalized * this.explosionForce * forceScale);
                rb.AddForce(disp.normalized * this.explosionForce * forceScale);
            }

            PlayerMovement pm = hit.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                //Debug.Log("colliding with " + hit.name + ": " + forceScale);
                pm.AddMoveForce(disp.normalized * this.pushbackForce * forceScale);
            }
        }
    }
}
