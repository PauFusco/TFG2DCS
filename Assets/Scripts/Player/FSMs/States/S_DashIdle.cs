namespace PFSM
{
    public class DIdleState : BaseState
    {
        private readonly float cooldownFrames;

        private float currentFrame;

        public DIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float cooldownFrames)
            : base(parentFSM, player)
        {
            this.cooldownFrames = cooldownFrames;
            currentFrame = this.cooldownFrames;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            if (currentFrame >= cooldownFrames &&
                input.dash == CustomInputControl.KeyState.DOWN)
            {
                return DashFSM.dash;
            }

            return DashFSM.idle;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }
    }
}