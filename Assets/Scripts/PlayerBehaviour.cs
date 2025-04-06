using PFSM;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    private float
        speed,
        dashSpeed,
        jumpForce,
        dashCooldown,
        dashDuration,
        jumpMaxDuration,
        fallGravityMultiplier;

    private PlayerFSM[] PFSMs;

    public bool jumping;
    public bool invulnerable;
    public bool lookDirection;

    public readonly uint moveFSMIdx = 0;
    public readonly uint jumpFSMIdx = 1;
    public readonly uint dashFSMIdx = 2;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        PlayerConfig config = GetComponent<PlayerConfig>();

        speed = config.speed;
        dashSpeed = config.dashSpeed;
        jumpForce = config.jumpForce;
        dashCooldown = config.dashCooldown;
        dashDuration = config.dashDuration;
        jumpMaxDuration = config.jumpMaxDuration;
        fallGravityMultiplier = config.fallGravityMultiplier;

        PFSMs = new PlayerFSM[3];

        MovementFSM moveFSM = new(this);
        DashFSM dashFSM = new(this, dashCooldown, dashDuration);
        JumpFSM jumpFSM = new(this, fallGravityMultiplier, jumpMaxDuration);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;

        jumping = false;
        invulnerable = false;
        lookDirection = true;
    }

    private void Update()
    {
        foreach (PlayerFSM FSM in PFSMs)
        { FSM.Update(); }
    }

    private void FixedUpdate()
    {
        foreach (PlayerFSM FSM in PFSMs)
        { FSM.FixedUpdate(); }
    }

    public void SetSpeed(Vector2 value)
    { rigidBody.linearVelocity = value * speed; }

    public void SetSpeedX(float value)
    { rigidBody.linearVelocityX = value * speed; }

    public void SetSpeedY(float value)
    { rigidBody.linearVelocityY = value * speed; }

    public void Jump()
    { rigidBody.linearVelocityY = jumpForce; }

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