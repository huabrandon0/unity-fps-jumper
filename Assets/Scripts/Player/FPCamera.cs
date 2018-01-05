// Usage: this script is meant to be placed on a Camera.
// The Camera must be a child of a Player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamera : TakesInput {

    // Input state
    private float xRot;
    private float yRot;
    private bool zoom;

    // Default state
    private Quaternion defaultPlayerRot;
    private Quaternion defaultCamRot;
    private float defaultXRot;
    private float defaultYRot;

    // Inconstant member variables
    [SerializeField] private Transform playerTransform; // Transform used to rotate around y-axis
    public bool IsZoomed { get; private set; }
    [SerializeField] private GameObject scopeOverlay;
    [SerializeField] private Camera viewmodelCam;

    // Constant member variables
    [SerializeField] private Camera cam; // Camera used to change FOV
    [SerializeField] private WeaponManager wmScript;
    [SerializeField] private float minimumX = -89f;
    [SerializeField] private float maximumX = 89f;
    //[SerializeField] private float sensitivity = 1f;


    protected override void GetInput()
    {
        if (!this.canReadInput)
            return;

        // Retrieve mouse input
        float sens = this.IsZoomed ? InputManager.instance.ZoomSensitivity : InputManager.instance.Sensitivity;
        this.xRot -= Input.GetAxis("Mouse Y") * sens;
        this.yRot += Input.GetAxis("Mouse X") * sens;

        this.zoom = InputManager.instance.GetKeyDown("Zoom");
    }

    protected override void ClearInput()
    {
        // The rotation of the camera is an accumulation of previous mouse 
        // inputs, so we cannot "clear" inputs without resetting the rotation.

        this.zoom = false;
    }

    protected override void GetDefaultState()
    {
        this.defaultPlayerRot = this.transform.parent.localRotation;
        this.defaultCamRot = this.transform.localRotation;
        this.defaultXRot = 0f;
        this.defaultYRot = 0f;
    }

    protected override void SetDefaultState()
    {
        ClearInput();
        this.playerTransform.localRotation = this.defaultPlayerRot;
        this.transform.localRotation = this.defaultCamRot;
        this.xRot = this.defaultXRot;
        this.yRot = this.defaultYRot;
        RefreshFov();
        Unzoom();
    }

    void Awake()
    {
        GetDefaultState();
        if (playerTransform == null)
            Debug.LogError(GetType() + ": player transform is not initialized.");
    }

    void Start()
    {
        SetDefaultState();
    }

    void Update()
    {
        GetInput();

        // Bound x-axis camera rotation
        this.xRot = Mathf.Clamp(this.xRot, this.minimumX, this.maximumX);

        // Rotate the player transform around the y-axis, the camera transform around the x-axis
        this.playerTransform.localRotation = Quaternion.Euler(0f, this.yRot, 0f);

        this.transform.localRotation = Quaternion.Euler(this.xRot, 0f, this.transform.localRotation.eulerAngles.z);


        // Zoom Toggle
        if (this.zoom && this.wmScript.GetCurrentWeapon().isZoomable)
        {
            if (this.IsZoomed)
                Unzoom();
            else
                Zoom();
        }
    }

    public void Zoom()
    {
        if (this.IsZoomed)
            return;

        this.IsZoomed = true;
        this.cam.fieldOfView = InputManager.instance.ZoomFov;
        this.scopeOverlay.SetActive(true);
        this.viewmodelCam.enabled = false;
    }

    public void Unzoom()
    {
        if (!this.IsZoomed)
            return;

        this.IsZoomed = false;
        this.cam.fieldOfView = InputManager.instance.Fov;
        this.scopeOverlay.SetActive(false);
        this.viewmodelCam.enabled = true;
    }

    public void RefreshFov()
    {
        if (this.IsZoomed)
            this.cam.fieldOfView = InputManager.instance.ZoomFov;
        else
            this.cam.fieldOfView = InputManager.instance.Fov;
    }
}
