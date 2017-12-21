// Usage: this script is meant to be placed on a Player.
// The Player must have a CharacterController component.
// The Player must have a PlayerController script to change friction constants. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStances : TakesInput {

    // Input state
    private bool crouchKeyDown = false;
    private bool crouchKeyUp = false;

    // Default state
    private float defaultHeight;

    // Inconstant member variables
    [SerializeField] private CharacterController characterController;

    private bool isCrouching = false;
    private bool targetStanceHasChanged = false;
    private Coroutine stanceCoroutine = null;

    private float standHeight;
    private float crouchHeight;
    private float targetHeight;

    // Constant member variables
    [SerializeField] private PlayerMovement movementScript;

    [SerializeField] private float transitionSpeed = 8f;    // Speed at which the player transitions between stances


    protected override void GetInput()
    {
        if (!this.canReadInput)
        {
            return;
        }

        this.crouchKeyDown = InputManager.GetKeyDown("Crouch");
        this.crouchKeyUp = InputManager.GetKeyUp("Crouch");
    }

    protected override void ClearInput()
    {
        this.crouchKeyDown = false;
        this.crouchKeyUp = false;
    }

    protected override void GetDefaultState()
    {
        this.defaultHeight = this.characterController.height;
    }

    protected override void SetDefaultState()
    {
        ClearInput();

        this.isCrouching = false;
        this.targetStanceHasChanged = false;
        if (this.stanceCoroutine != null)
        {
            StopCoroutine(this.stanceCoroutine);
            this.stanceCoroutine = null;
        }

        this.characterController.height = this.defaultHeight;
        this.standHeight = this.characterController.height;
        this.crouchHeight = this.standHeight / 2f;
        this.targetHeight = this.standHeight;
    }

    void Awake()
    {
        GetDefaultState();
    }

    void OnEnable()
    {
        SetDefaultState();
    }

    void Update()
    {
        GetInput();

        if (this.crouchKeyDown)
            Crouch();
        else if (this.crouchKeyUp)
            Uncrouch();

        if (this.targetStanceHasChanged)
        {
            // The coroutine to change stances must be stopped if still running
            if (this.stanceCoroutine != null)
                StopCoroutine(this.stanceCoroutine);   // This could possibly attempt to stop a coroutine after it has ended. Bad practice?

            this.stanceCoroutine = StartCoroutine(GoToTargetStance());
        }
    }

    void Crouch()
    {
        if (!this.isCrouching)
        {
            this.targetStanceHasChanged = true;
            this.isCrouching = true;
            this.targetHeight = this.crouchHeight;
            this.movementScript.SetFriction(1);
        }
    }

    void Uncrouch()
    {
        if (this.isCrouching)
        {
            this.targetStanceHasChanged = true;
            this.isCrouching = false;
            this.targetHeight = this.standHeight;
            this.movementScript.SetFriction(0);
        }
    }

    IEnumerator GoToTargetStance()
    {
        this.targetStanceHasChanged = false;

        float startHeight = this.characterController.height;
        float startTime = Time.time;
        float interpVal = 0f;

        while (interpVal < 1f)
        {
            yield return null;

            // Update the interpolation value used for lerp
            interpVal = (Time.time - startTime) * this.transitionSpeed;

            // Store the Player's position and height before any stance changes
            Vector3 lastPos = this.transform.localPosition;
            float lastHeight = this.characterController.height;

            // Adjust the Player's height at a constant rate
            this.characterController.height = Mathf.Lerp(startHeight, this.targetHeight, interpVal);

            // Adjust the Player's position such that bottom of the player remains at the same height
            lastPos[1] += (this.characterController.height - lastHeight) * 0.5f;
            this.transform.localPosition = lastPos;
        }
    }
}
