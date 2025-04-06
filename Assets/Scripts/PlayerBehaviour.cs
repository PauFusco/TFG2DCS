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
        fallGravityMultiplier;

    private PlayerFSM[] PFSMs;

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
        fallGravityMultiplier = config.fallGravityMultiplier;

        PFSMs = new PlayerFSM[3];

        MovementFSM moveFSM = new(this);
        JumpFSM jumpFSM = new(this, fallGravityMultiplier);
        DashFSM dashFSM = new(this, dashCooldown, dashDuration);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;

        invulnerable = true;
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

    public void Move(Vector2 value)
    { rigidBody.linearVelocityX = value.x * speed; }

    public void Jump()
    {
        rigidBody.linearVelocityY = jumpForce;
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