using CustomInputControl;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject floor;

    private float
        maxSpeed,
        baseGravity,
        accelerationFrames,
        decelerationFrames,
        dashSpeed,
        dashFrames,
        dashCooldownFrames,
        jumpSpeed,
        jumpHeight,
        jumpCutoffFrames,
        fallGravity,
        chargeFrames;

    private PlayerConfig config;
    private PlayerInputController inputController;
    private SpriteRenderer spriteRenderer;

    private PFSM.PlayerFSM[] PFSMs;

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
        config = GetComponent<PlayerConfig>();
        inputController = GetComponent<PlayerInputController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdatePlayerConfig();

        PFSMs = new PFSM.PlayerFSM[4];

        PFSM.MovementFSM moveFSM = new(
            this,
            maxSpeed,
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
            this,
            config.attacks,
            chargeFrames);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;
        PFSMs[attaFSMIdx] = attaFSM;

        #region Debug Variables
        currentSpeed = .0f;

        grounded = false;
        invulnerable = false;
        lookDirection = true;

        currentMoveState = PFSM.MoveStateE.Default;
        currentJumpState = PFSM.JumpStateE.Default;
        #endregion
    }

    private void Update()
    {
        UpdatePlayerConfig();

        SendInput();

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

    private void UpdatePlayerConfig()
    {
        maxSpeed = config.maxSpeed;
        baseGravity = config.baseGravity;

        accelerationFrames = config.accelerationFrames;
        decelerationFrames = config.decelerationFrames;

        dashSpeed = config.dashSpeed;
        dashFrames = config.dashFrames;
        dashCooldownFrames = config.dashCooldownFrames;

        jumpSpeed = config.jumpSpeed;
        jumpHeight = config.jumpHeight;
        jumpCutoffFrames = config.jumpCutoffFrames;
        fallGravity = config.fallGravityMultiplier;

        chargeFrames = config.chargeFrames;

        currentSpeed = rigidBody.linearVelocityX;
    }

    void SendInput()
    {
        foreach (var FSM in PFSMs)
        {
            FSM.HandleInput(inputController.input);
        }
    }

    public void SetPlayerColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetSpeedX(float value)
    { rigidBody.linearVelocityX = value; }

    public void SetSpeedY(float value)
    { rigidBody.linearVelocityY = value; }

    public void Dash(float speed)
    { SetSpeedX(speed); }



    public PFSM.PlayerFSM GetFSM(uint index)
    { return PFSMs[index]; }

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