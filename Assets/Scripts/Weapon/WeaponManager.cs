using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : TakesInput {
    
    // Input state
    private bool switchingWeps;
    private int switchWepIndex;

    // Default state
    private List<Weapon> defWeps;

    // Inconstant member variables
    private Weapon currentWeapon;
    private GameObject weaponModel;
    private WeaponEffects weaponEffects;
    private Animator weaponAnimator;

    public List<Weapon> weps;
    private int currWepIndex;

    // Constant member variables
    [SerializeField] private PlayerShoot shootScript;
    [SerializeField] private FPCamera camScript;
    [SerializeField] private Transform weaponBase;
    [SerializeField] private string viewmodelLayerName = "Viewmodel";


    protected override void GetInput()
    {
        if (!this.canReadInput)
            return;

        for (int i = 0; i < weps.Count; i++)
        {
            if (InputManager.instance.GetKeyDown("Weapon" + (i + 1)))
            {
                this.switchingWeps = true;
                this.switchWepIndex = i;
            }
        }
    }

    protected override void ClearInput()
    {
        this.switchingWeps = false;
    }

    protected override void GetDefaultState()
    {
        this.defWeps = new List<Weapon>(this.weps);
    }

    protected override void SetDefaultState()
    {
        ClearInput();
        this.weps = new List<Weapon>(this.defWeps);
        this.currWepIndex = 0;
        EquipWeapon(this.weps[0]);
    }

    void Awake()
    {
        GetDefaultState();

        if (this.shootScript == null)
        {
            Debug.LogError(GetType() + ": no shoot script assigned");
            this.enabled = false;
        }

        if (this.camScript == null)
        {
            Debug.LogError(GetType() + ": no camera script assigned");
            this.enabled = false;
        }
    }

    void OnEnable()
    {
        SetDefaultState();
    }

    void Update()
    {
        GetInput();

        if (this.switchingWeps && this.currWepIndex != this.switchWepIndex)
        {
            EquipWeapon(weps[this.switchWepIndex]);
            this.currWepIndex = this.switchWepIndex;
            this.shootScript.DisableShooting();
        }
        else if (!this.shootScript.CanShoot)
        {
            // Probably should put a timer to disable/enable shooting based on weapon takeout time
            this.shootScript.EnableShooting();
        }
    }
    
    void EquipWeapon (Weapon weapon)
    {
        // Set currentWeapon to new weapon, weaponModel to the new weapon's model
        this.currentWeapon = weapon;
        Destroy(this.weaponModel); // Note: it may be inefficient to destroy the model entirely (it can be re-used!)
        this.weaponModel = Instantiate(this.currentWeapon.weaponModel) as GameObject;
        this.weaponModel.transform.SetParent(this.weaponBase, false);
        
        // Set the newly instantiated weaponModel to the correct layer
        Util.SetLayersRecursively(this.weaponModel, LayerMask.NameToLayer(this.viewmodelLayerName));
        
        // Set weaponEffects to the new weapon's WeaponEffects
        this.weaponEffects = this.weaponModel.GetComponent<WeaponEffects>();
        if (this.weaponEffects == null)
            Debug.LogError(GetType() + ": weapon model does not have a WeaponEffects script attached");

        this.weaponAnimator = this.weaponModel.GetComponent<Animator>();
        if (this.weaponAnimator == null)
            Debug.LogError(GetType() + ": weapon model does nto have an Animator component");

        if (this.camScript.IsZoomed)
            this.camScript.Unzoom();
    }

    public Weapon GetCurrentWeapon()
    {
        return this.currentWeapon;
    }

    public WeaponEffects GetCurrentWeaponEffects()
    {
        return this.weaponEffects;
    }

    public Animator GetCurrentWeaponAnimator()
    {
        return this.weaponAnimator;
    }
}
