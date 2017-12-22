using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float explosionRadius = 4f;
    public float explosionForce = 200f;
    public float pushbackForce = 15f;
    public LayerMask affectedLayers;

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);

        var hitColliders = Physics.OverlapSphere(this.transform.position, this.explosionRadius, this.affectedLayers);
        bool hasHitPlayer = false;
        foreach (Collider hit in hitColliders)
        {
            // Check to prevent double-hits on Player (don't want 2x knockback)
            if (hit.tag == "Player")
            {
                if (hasHitPlayer)
                    continue;
                
                hasHitPlayer = true;
            }

            Vector3 disp = hit.transform.position - this.transform.position;
            float forceScale = Mathf.Clamp((disp.magnitude != 0) ? Mathf.Cos(Mathf.PI * disp.magnitude / (1.5f * explosionRadius)) : 0, 0, 1);

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(disp.normalized * this.explosionForce * forceScale);

            PlayerMovement pm = hit.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.AddMoveForce(disp.normalized * this.pushbackForce * forceScale);
        }
    }
}
