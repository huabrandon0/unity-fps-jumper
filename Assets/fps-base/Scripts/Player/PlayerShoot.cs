// Usage: this script is meant to be placed on a Player.
// The Player must be assigned a Camera to shoot from.
// A WeaponManager component must be present.
// An FPCamera script must be assigned for its zooming capabilities.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : TakesInput {

    // Input state
    private bool shootKeyDown = false;
    private bool shootKeyUp = false;
    private bool zoom = false;

    // Inconstant member variables
    public bool CanShoot { get; private set; }
    private bool isShooting = false;
    private Coroutine shootCoroutine = null;
    private Weapon currentWeapon;
    private float timeLastShot = float.MinValue;

    // Constant member variables
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private LayerMask shootableMask;
    [SerializeField] private Camera camToShootFrom;
    [SerializeField] private FPCamera camScript;
    [SerializeField] private float projectileSpawnOffset = 1.0f;

    protected override void GetInput()
    {
        if (!this.canReadInput)
            return;

        this.shootKeyDown = InputManager.GetKeyDown("Attack1");
        this.shootKeyUp = InputManager.GetKeyUp("Attack1");

        this.zoom = InputManager.GetKeyDown("Zoom");
    }

    protected override void ClearInput()
    {
        this.shootKeyDown = false;
        this.shootKeyUp = false;
    }

    protected override void GetDefaultState(){}

    protected override void SetDefaultState()
    {
        ClearInput();

        this.CanShoot = true;
        this.isShooting = false;
        StopShootCoroutine();
        this.timeLastShot = float.MinValue;

        this.currentWeapon = this.weaponManager.GetCurrentWeapon();
    }

    void Awake()
    {
        GetDefaultState();

        if (this.camToShootFrom == null)
            Debug.LogError(GetType() + ": no camera assigned");

        if (this.weaponManager == null)
            Debug.LogError(GetType() + ": no weapon manager assigned");
    }

    void OnEnable()
    {
        SetDefaultState();
    }

    void Update()
    {
        GetInput();

        this.currentWeapon = this.weaponManager.GetCurrentWeapon();
        if (this.camScript.IsZoomed && !this.currentWeapon.isZoomable)
        {
            this.camScript.Unzoom();
        }

        // Shoot
        if (this.CanShoot)
        {
            if (this.currentWeapon.fireRate <= 0f)
            {
                // Tap fire
                if (this.shootKeyDown && (Time.time >= this.timeLastShot + this.currentWeapon.cooldown))
                {
                    Shoot();
                    this.timeLastShot = Time.time;
                }
            }
            else
            {
                // Automatic fire
                if (this.shootKeyDown && this.isShooting == false)
                {
                    this.isShooting = true;
                    StartShootCoroutine();
                }
                else if (this.shootKeyUp && this.isShooting == true)
                {
                    this.isShooting = false;
                    StopShootCoroutine();
                }
            }
        }

        // Zoom
        if (this.zoom && this.currentWeapon.isZoomable)
        {
            if (this.camScript.IsZoomed)
                this.camScript.Unzoom();
            else
                this.camScript.Zoom();
        }
    }

    void Shoot()
    {
        // Play muzzle flash
        ShootEffects();

        if (currentWeapon.type == "hitscan")
        {
            RaycastHit hit;
            if (Physics.Raycast(this.camToShootFrom.transform.position, this.camToShootFrom.transform.forward, out hit, this.currentWeapon.range, this.shootableMask))
            {
                // Play hit effect
                HitEffects(hit.point, hit.normal);
            }
        }
        else if (currentWeapon.type == "projectile")
        {
            GameObject projectile = Instantiate(this.currentWeapon.projectile,
                this.camToShootFrom.transform.position + this.camToShootFrom.transform.forward * this.projectileSpawnOffset,
                Quaternion.LookRotation(this.camToShootFrom.transform.forward));
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * this.currentWeapon.projectileSpeed;
  
            // Note: remember to set up Physics to ignore layer collisions between Projectile and the following:
            // Player, Viewmodel, Projectile, TriggerZone

            Destroy(projectile, 3.0f);
        }
        else
            Debug.LogError(GetType() + ": weapon is of unknown type. Unable to shoot.");
    }

    void ShootEffects()
    {
        this.weaponManager.GetCurrentWeaponEffects().muzzleFlash.Play();
    }

    void HitEffects(Vector3 pos, Vector3 normal)
    {
        GameObject bulletImpact = Instantiate(this.weaponManager.GetCurrentWeaponEffects().bulletImpact, pos, Quaternion.LookRotation(normal));
        Destroy(bulletImpact, 1f);
    }

    private IEnumerator ShootAutomatic(float timeBetweenShots)
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void StartShootCoroutine()
    {
        this.shootCoroutine = StartCoroutine(ShootAutomatic(1f / this.currentWeapon.fireRate));
    }

    private void StopShootCoroutine()
    {
        if (this.shootCoroutine != null)
        {
            StopCoroutine(this.shootCoroutine);
            this.shootCoroutine = null;
        }
    }

    public void DisableShooting()
    {
        this.CanShoot = false;
        StopShootCoroutine();
    }

    public void EnableShooting()
    {
        this.CanShoot = true;
    }
}
