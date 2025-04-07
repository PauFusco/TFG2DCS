using PFSM;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    private float
        maxSpeed,
        turnFrames,
        accelerationFrames,
        decelerationFrames,
        dashSpeed,
        dashFrames,
        dashCooldownFrames,
        jumpHeight,
        jumpMaxFrames,
        jumpCutoffFrames,
        fallDurationFrames,
        fallGravityMultiplier;

    private PlayerFSM[] PFSMs;

    public float currentSpeed;
    public bool jumping;
    public bool invulnerable;
    public bool lookDirection;

    public readonly uint moveFSMIdx = 0;
    public readonly uint jumpFSMIdx = 1;
    public readonly uint dashFSMIdx = 2;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        UpdatePlayerData();

        PFSMs = new PlayerFSM[3];

        MovementFSM moveFSM = new(
            this,
            maxSpeed,
            turnFrames,
            accelerationFrames,
            decelerationFrames);

        DashFSM dashFSM = new(this, dashCooldownFrames, dashFrames);
        JumpFSM jumpFSM = new(this, fallGravityMultiplier, jumpMaxFrames);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;

        currentSpeed = .0f;
        jumping = false;
        invulnerable = false;
        lookDirection = true;
    }

    private void Update()
    {
        UpdatePlayerData();

        foreach (PlayerFSM FSM in PFSMs)
        { FSM.Update(); }
    }

    private void FixedUpdate()
    {
        foreach (PlayerFSM FSM in PFSMs)
        { FSM.FixedUpdate(); }
    }

    private void UpdatePlayerData()
    {
        PlayerConfig config = GetComponent<PlayerConfig>();

        maxSpeed = config.maxSpeed;
        turnFrames = config.turnFrames;
        accelerationFrames = config.accelerationFrames;
        decelerationFrames = config.decelerationFrames;

        dashSpeed = config.dashSpeed;
        dashFrames = config.dashFrames;
        dashCooldownFrames = config.dashCooldownFrames;

        jumpHeight = config.jumpHeight;
        jumpMaxFrames = config.jumpMaxFrames;
        jumpCutoffFrames = config.jumpCutoffFrames;
        fallDurationFrames = config.fallDurationFrames;
        fallGravityMultiplier = config.fallGravityMultiplier;

        currentSpeed = rigidBody.linearVelocityX;
    }

    public void SetSpeed(Vector2 value)
    { rigidBody.linearVelocity = value; }

    public void SetSpeedX(float value)
    { rigidBody.linearVelocityX = value; }

    public void SetSpeedY(float value)
    { rigidBody.linearVelocityY = value; }

    public void Jump()
    {
        //rigidBody.linearVelocityY = jumpForce;
    }

    public void Dash()
    {
        if (lookDirection) rigidBody.linearVelocityX = dashSpeed;
        else rigidBody.linearVelocityX = -dashSpeed;
    }

    public PlayerFSM GetFSM(uint index)
    { return PFSMs[index]; }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    { PFSMs[moveFSMIdx].HandleInput(this, ctx); }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    { PFSMs[jumpFSMIdx].HandleInput(this, ctx); }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    { PFSMs[dashFSMIdx].HandleInput(this, ctx); }
}