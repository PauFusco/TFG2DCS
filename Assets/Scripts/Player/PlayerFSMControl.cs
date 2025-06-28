using UnityEngine;

public class PlayerFSMControl : MonoBehaviour
{
    private PlayerConfig config;
    private CustomInputControl.PlayerInputController inputController;

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
        minChargeFrames,
        effectiveChargeFrames,
        maxChargeFrames;

    private PFSM.PlayerFSM[] PFSMs;

    private bool paused;

    public PFSM.MoveStateE currentMoveState = new();
    public PFSM.JumpStateE currentJumpState = new();

    public static readonly uint moveFSMIdx = 0;
    public static readonly uint jumpFSMIdx = 1;
    public static readonly uint dashFSMIdx = 2;
    public static readonly uint attaFSMIdx = 3;

    private void Awake()
    {
        config = GetComponent<PlayerConfig>();
        inputController = GetComponent<CustomInputControl.PlayerInputController>();
        
        UpdatePlayerConfig();

        var player = GetComponent<PlayerBehaviour>();

        PFSMs = new PFSM.PlayerFSM[4];

        PFSM.MovementFSM moveFSM = new(
            player,
            maxSpeed,
            accelerationFrames,
            decelerationFrames);
        PFSM.DashFSM dashFSM = new(
            player,
            dashSpeed,
            dashCooldownFrames,
            dashFrames);
        PFSM.JumpFSM jumpFSM = new(
            player,
            jumpSpeed,
            jumpHeight,
            jumpCutoffFrames,
            baseGravity,
            fallGravity);
        PFSM.AttackFSM attaFSM = new(
            player,
            config.attacks,
            minChargeFrames,
            effectiveChargeFrames,
            maxChargeFrames);

        PFSMs[moveFSMIdx] = moveFSM;
        PFSMs[jumpFSMIdx] = jumpFSM;
        PFSMs[dashFSMIdx] = dashFSM;
        PFSMs[attaFSMIdx] = attaFSM;

        #region Debug Variables
        currentMoveState = PFSM.MoveStateE.Default;
        currentJumpState = PFSM.JumpStateE.Default;
        #endregion
    }

    private void Update()
    {
        UpdatePlayerConfig();

        if (!paused)
        {
            SendInputToFSMs();

            foreach (PFSM.PlayerFSM FSM in PFSMs)
            { FSM.Update(); }
        }

        currentMoveState = PFSMs[moveFSMIdx].currentState.thisMoveState;
        currentJumpState = PFSMs[jumpFSMIdx].currentState.thisJumpState;
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            foreach (PFSM.PlayerFSM FSM in PFSMs)
            { FSM.FixedUpdate(); }
        }
    }

    public void Pause()
    {
        paused = true;

        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.linearVelocity = Vector2.zero;
    }

    public void UnPause()
    {
        paused = false;

        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = baseGravity;
    }


    public void ResetFSMs()
    {
        ((PFSM.MovementFSM)PFSMs[moveFSMIdx]).currentState = PFSM.MovementFSM.idle;
        ((PFSM.JumpFSM)PFSMs[jumpFSMIdx]).currentState = PFSM.JumpFSM.freeFall;
        ((PFSM.DashFSM)PFSMs[dashFSMIdx]).currentState = PFSM.DashFSM.idle;
        ((PFSM.AttackFSM)PFSMs[attaFSMIdx]).currentState = PFSM.AttackFSM.idle;
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

        minChargeFrames = config.minChargeFrames;
        effectiveChargeFrames = config.effectiveChargeFrames;
        maxChargeFrames = config.maxChargeFrames;
    }

    public void SendInputToFSMs()
    {
        foreach(var FSM in PFSMs)
        {
            FSM.HandleInput(inputController.input);
        }
    }

    public PFSM.PlayerFSM GetFSM(uint index)
    { return PFSMs[index]; }
}
