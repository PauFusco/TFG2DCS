using UnityEngine;

namespace PFSM
{
    public class AccelerateState : BaseState
    {
        private readonly float maxSpeed;
        private readonly float accelerationFrames;

        private float currentFrame;

        public float targetSpeed;

        public AccelerateState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed,
            float accelerationFrames)
            : base(parentFSM, player)
        {
            this.maxSpeed = maxSpeed;
            this.accelerationFrames = accelerationFrames;
            thisMoveState = MoveStateE.ACCELERATE;

            currentFrame = .0f;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            Vector2 noMove = new(.0f, .0f);
            if (input.movement != noMove)
            {
                player.lookDirection = input.movement.x >= 0;

                SetData(input.movement.x);
            }
            else
            {
                return MovementFSM.decelerate;
            }

            return MovementFSM.accelerate;
        }

        public void SetData(float targetSpeedMult)
        {
            targetSpeed = targetSpeedMult * maxSpeed;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= accelerationFrames)
            {
                MovementFSM.walk.SetData(targetSpeed);

                parentFSM.ChangeState(MovementFSM.walk);
            }

            if (player.GetFSM(PlayerFSMControl.dashFSMIdx).currentState == DashFSM.idle &&
                player.GetFSM(PlayerFSMControl.attaFSMIdx).currentState == AttackFSM.idle)
                player.SetSpeedX(targetSpeed / accelerationFrames * currentFrame);
            else
                parentFSM.ChangeState(MovementFSM.decelerate);
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }
    }
}