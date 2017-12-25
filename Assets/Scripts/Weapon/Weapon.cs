using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class Weapon {

    public string name = "Pistol";
    public string type = "hitscan";
    public int damage = 10;
    public float range = 200f;
    public float fireRate = 0f; // fireRate <= 0: tap-fire; fireRate > 0: automatic fire with fireRate shots/s
    public float cooldown = 0f;
    public bool isZoomable = false;
    public GameObject weaponModel = null;

    public GameObject projectile = null;
    public float projectileSpeed = 5f;


    void Awake()
    {
        if (this.weaponModel == null)
            Debug.LogError(GetType() + ": no weapon model assigned");
    }
}
