using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponEffects : MonoBehaviour {

    public ParticleSystem MuzzleFlash { get; private set; }
    public GameObject BulletImpactPrefab { get; private set; }

    [SerializeField] private GameObject bulletImpactPrefab = null;
    [SerializeField] private GameObject muzzleFlashPrefab = null;
    [SerializeField] private Transform gunBarrelEnd = null; 

    void Awake()
    {
        if (this.muzzleFlashPrefab == null)
        {
            Debug.LogError(GetType() + ": no muzzle flash prefab assigned");
            this.enabled = false;
        }
        
        if (this.bulletImpactPrefab == null)
        {
            Debug.LogError(GetType() + ": no bullet impact particle system assigned");
            this.enabled = false;
        }

        if (this.gunBarrelEnd == null)
        {
            Debug.LogError(GetType() + ": no gun barrel end transform assigned");
            this.enabled = false;
        }

        this.MuzzleFlash = Instantiate(muzzleFlashPrefab, this.gunBarrelEnd, false).GetComponent<ParticleSystem>();
        this.BulletImpactPrefab = this.bulletImpactPrefab;
    }
}
