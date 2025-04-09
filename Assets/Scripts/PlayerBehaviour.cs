using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private float
        maxSpeed,
        maxAirSpeed,
        baseGravity,
        turnFrames,
        accelerationFrames,
        decelerationFrames,
        dashSpeed,
        dashFrames,
        dashCooldownFrames,
        jumpSpeed,
        jumpHeight,
        jumpCutoffFrames,
        fallGravity;

    private PFSM.PlayerFSM[] PFSMs;

    [SerializeField] private GameObject floor;

    public Rigidbody2D rigidBody;

    public PFSM.MoveStateE currentMoveState = new();
    public PFSM.JumpStateE currentJumpState = new();

    public float currentSpeed;
    public bool grounded;
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
            maxAirSpeed,
            turnFrames,
            accelerationFrames,
            decelerationFrames);
        PFSM.DashFSM dashFSM = new(
            this,
            dashSpeed,
            dashCooldownFrames,
            dashFrames);
        PFSM.JumpFSM jumpFSM = new(
            this,
            jumpSpeed,
            jumpHeight,
            jumpCutoffFrames,
            baseGravity,
            fallGravity);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;

        currentSpeed = .0f;

        grounded = false;
        invulnerable = false;
        lookDirection = true;

        currentMoveState = PFSM.MoveStateE.Default;
        currentJumpState = PFSM.JumpStateE.Default;
    }

    private void Update()
    {
        UpdatePlayerData();

        currentMoveState = PFSMs[moveFSMIdx].currentState.thisMoveState;
        currentJumpState = PFSMs[jumpFSMIdx].currentState.thisJumpState;

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
        maxAirSpeed = config.maxAirSpeed;
        baseGravity = config.baseGravity;

        turnFrames = config.turnFrames;
        accelerationFrames = config.accelerationFrames;
        decelerationFrames = config.decelerationFrames;

        dashSpeed = config.dashSpeed;
        dashFrames = config.dashFrames;
        dashCooldownFrames = config.dashCooldownFrames;

        jumpSpeed = config.jumpSpeed;
        jumpHeight = config.jumpHeight;
        jumpCutoffFrames = config.jumpCutoffFrames;
        fallGravity = config.fallGravityMultiplier;

        currentSpeed = rigidBody.linearVelocityX;
    }

    public void SetSpeed(Vector2 value)
    { rigidBody.linearVelocity = value; }

    public void SetSpeedX(float value)
    { rigidBody.linearVelocityX = value; }

    public void SetSpeedY(float value)
    { rigidBody.linearVelocityY = value; }

    public void Dash(float speed)
    { SetSpeedX(speed); }

    public PFSM.PlayerFSM GetFSM(uint index)
    { return PFSMs[index]; }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    { PFSMs[moveFSMIdx].HandleInput(this, ctx); }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    { PFSMs[jumpFSMIdx].HandleInput(this, ctx); }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    { PFSMs[dashFSMIdx].HandleInput(this, ctx); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(floor))
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(floor))
            grounded = false;
    }
}