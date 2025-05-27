namespace PFSM
{
    public class GroundState : BaseState
    {
        public GroundState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        {
            thisJumpState = JumpStateE.GROUND;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            if (input.jump == CustomInputControl.KeyState.REPEAT &&
                !player.airborne &&
                player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle)
            {
                return JumpFSM.jump;
            }

            return JumpFSM.ground;
        }

        public override void OnEnter()
        { }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}