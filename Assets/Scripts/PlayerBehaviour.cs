using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private float
        maxSpeed,
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

    public PAttack.Attack[] attacks;

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
    public readonly uint attaFSMIdx = 3;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        UpdatePlayerData();

        PFSMs = new PFSM.PlayerFSM[4];

        PFSM.MovementFSM moveFSM = new(
            this,
            maxSpeed,
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
        PFSM.AttackFSM attaFSM = new(
            this);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;
        PFSMs[attaFSMIdx] = attaFSM;

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

        attacks = config.attacks;

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
    {
        PFSMs[moveFSMIdx].HandleInput(ctx);
        Debug.Log(ctx.action.name);
    }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    { PFSMs[jumpFSMIdx].HandleInput(ctx); }

    public void HandleDashInput(InputAction.CallbackContext ctx)
    { PFSMs[dashFSMIdx].HandleInput(ctx); }

    public void HandleAttackInput(InputAction.CallbackContext ctx)
    { Debug.Log(ctx.action.name); }

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