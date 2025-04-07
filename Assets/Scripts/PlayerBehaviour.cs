using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
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

    private PFSM.PlayerFSM[] PFSMs;

    public Rigidbody2D rigidBody;

    public PFSM.State currentState = new();

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

        PFSMs = new PFSM.PlayerFSM[3];

        PFSM.MovementFSM moveFSM = new(
            this,
            maxSpeed,
            turnFrames,
            accelerationFrames,
            decelerationFrames);

        PFSM.DashFSM dashFSM = new(this, dashCooldownFrames, dashFrames);
        PFSM.JumpFSM jumpFSM = new(this, fallGravityMultiplier, jumpMaxFrames);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;

        currentSpeed = .0f;
        jumping = false;
        invulnerable = false;
        lookDirection = true;
        currentState = PFSM.State.IDLE;
    }

    private void Update()
    {
        UpdatePlayerData();

        currentState = PFSMs[moveFSMIdx].currentState.thisState;

        foreach (PFSM.PlayerFSM FSM in PFSMs)
        { FSM.Update(); }
    }

    private void FixedUpdate()
    {       
        foreach (PFSM.PlayerFSM FSM in PFSMs)
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
    { }

    public void Dash()
    {
        if (lookDirection) rigidBody.linearVelocityX = dashSpeed;
        else rigidBody.linearVelocityX = -dashSpeed;
    }

    public PFSM.PlayerFSM GetFSM(uint index)
    { return PFSMs[index]; }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    { PFSMs[moveFSMIdx].HandleInput(this, ctx); }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    { PFSMs[jumpFSMIdx].HandleInput(this, ctx); }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    { PFSMs[dashFSMIdx].HandleInput(this, ctx); }
}