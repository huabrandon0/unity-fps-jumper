// Usage: this script is meant to be placed on a Player.
// The Player must have a CharacterController component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : TakesInput {

    // Input state
    private Vector2 inputVec;   // Horizontal movement input
    private bool jump;          // Whether the jump key is inputted

    // Inconstant member variables
    private Vector3 moveVec;                    // Vector3 used to move the character controller
    private float friction;
    private bool isJumping = false;             // Player has jumped and not been grounded yet
    private bool groundedLastFrame = false;    // Player was grounded during the last frame

    // Constant member variables
    private CharacterController charController;

    [SerializeField] private float gravityMultiplier = 1.6f;
    [SerializeField] private float stickToGroundForce = 1.6f;
    [SerializeField] private float[] frictionConstants = { 5f, 10f };
    [SerializeField] private float jumpSpeed = 5f;  // Initial upwards speed of the jump
    [SerializeField] private float groundAccel = 5f;
    [SerializeField] private float airAccel = 800f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxAirSpeed = 1.3f;


    protected override void GetInput()
    {
        if (!this.canReadInput)
            return;

        float up = InputManager.GetKey("Strafe Up") ? 1 : 0,
        left = InputManager.GetKey("Strafe Left") ? -1 : 0,
        down = InputManager.GetKey("Strafe Down") ? -1 : 0,
        right = InputManager.GetKey("Strafe Right") ? 1 : 0;

        this.inputVec = new Vector2(left + right, up + down);

        // TODO: InputManager currently doesn't support scroll wheel input, so we must hardcode it here
        if (!this.jump)
            this.jump = InputManager.GetKeyDown("Jump") || Input.GetAxisRaw("Mouse ScrollWheel") != 0;
    }

    protected override void ClearInput()
    {
        this.inputVec = Vector2.zero;
        this.jump = false;
    }

    protected override void GetDefaultState(){}

    protected override void SetDefaultState()
    {
        ClearInput();
        this.moveVec = Vector3.zero;
        this.friction = this.frictionConstants[0];
        this.isJumping = false;
        this.groundedLastFrame = false;
    }

    void Awake()
    {
        GetDefaultState();
        this.charController = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        SetDefaultState();
    }

    void Update()
    {
        GetInput();

        // Jump
        if (!this.groundedLastFrame && this.charController.isGrounded)
        {
            this.moveVec.y = 0f;
            this.isJumping = false;
        }

        if (!this.charController.isGrounded && !this.isJumping && this.groundedLastFrame)
        {
            this.moveVec.y = 0f;
        }

        // Horizontal movement
        if (this.inputVec.magnitude > 1)
            this.inputVec = this.inputVec.normalized;

        if (this.charController.isGrounded)
            MoveGround();
        else
            MoveAir();

        //Debug.Log("Speed: " + charController.velocity.magnitude);

        this.charController.Move(this.moveVec * Time.deltaTime);
        this.jump = false;
        this.groundedLastFrame = this.charController.isGrounded;
    }

    void MoveGround()
    {
        Vector3 wishVel = this.moveSpeed * (transform.forward * this.inputVec.y + this.transform.right * this.inputVec.x);
        Vector3 prevMove = new Vector3(this.moveVec.x, 0, this.moveVec.z);

        // Apply friction
        float speed = prevMove.magnitude;
        if (speed != 0) // To avoid divide by zero errors
        {
            // May implement some "sv_stopspeed"-like variable if low-speed gameplay feels too responsive 
            float drop = speed * this.friction * Time.deltaTime;
            float newSpeed = speed - drop;
            if (newSpeed < 0)
                newSpeed = 0;
            if (newSpeed != speed)
            {
                newSpeed /= speed;
                prevMove = prevMove * newSpeed;
            }

            wishVel -= (1.0f - newSpeed) * prevMove;
        }

        float wishSpeed = wishVel.magnitude;
        Vector3 wishDir = wishVel.normalized;

        Vector3 nextMove = GroundAccelerate(wishDir, prevMove, wishSpeed, this.groundAccel);
        nextMove.y = -this.stickToGroundForce;

        if (this.jump)
        {
            nextMove.y = this.jumpSpeed;
            this.jump = false;
            this.isJumping = true;
        }

        this.moveVec = nextMove;
    }

    Vector3 GroundAccelerate(Vector3 wishDir, Vector3 prevVelocity, float wishSpeed, float accel)
    {
        float currentSpeed = Vector3.Dot(prevVelocity, wishDir);
        float addSpeed = wishSpeed - currentSpeed;

        if (addSpeed <= 0)
            return prevVelocity;

        float accelSpeed = accel * wishSpeed * Time.deltaTime;

        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        return prevVelocity + accelSpeed * wishDir;
    }

    void MoveAir()
    {
        Vector3 wishVel = this.moveSpeed * (this.transform.forward * this.inputVec.y + transform.right * this.inputVec.x);
        float wishSpeed = wishVel.magnitude;
        Vector3 wishDir = wishVel.normalized;

        Vector3 prevMove = new Vector3(this.moveVec.x, 0, this.moveVec.z);

        Vector3 nextMove = AirAccelerate(wishDir, prevMove, wishSpeed, this.airAccel);
        nextMove.y = this.moveVec.y;
        nextMove += Physics.gravity * this.gravityMultiplier * Time.deltaTime;

        this.moveVec = nextMove;
    }

    Vector3 AirAccelerate(Vector3 wishDir, Vector3 prevVelocity, float wishSpeed, float accel)
    {
        if (wishSpeed > this.maxAirSpeed)
            wishSpeed = this.maxAirSpeed;

        float currentSpeed = Vector3.Dot(prevVelocity, wishDir);
        float addSpeed = wishSpeed - currentSpeed;

        if (addSpeed <= 0)
            return prevVelocity;

        float accelSpeed = accel * wishSpeed * Time.deltaTime;

        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        return prevVelocity + wishDir * accelSpeed;
    }

    public void SetFriction(int i)
    {
        if (i >= 0 && i < this.frictionConstants.Length)
            this.friction = this.frictionConstants[i];
    }

    public void AddMoveForce(Vector3 addVec)
    {
        this.moveVec += addVec;
    }
}
