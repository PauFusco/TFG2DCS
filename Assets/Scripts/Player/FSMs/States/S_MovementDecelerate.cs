using UnityEngine;

namespace PFSM
{
    public class DecelerateState : BaseState
    {
        private readonly float decelerationFrames;
        private readonly float maxSpeed;

        private float currentFrame;
        private float speedReductionPerFrame;

        public float speedToReduce;

        public DecelerateState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed,
            float decFrames)
            : base(parentFSM, player)
        {
            thisMoveState = MoveStateE.DECELERATE;
            decelerationFrames = decFrames;
            this.maxSpeed = maxSpeed;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            Vector2 noMove = new(.0f, .0f);
            if (input.movement != noMove)
            {
                float speedMult = input.movement.x;

                player.lookDirection = speedMult >= 0;

                MovementFSM.accelerate.SetData(speedMult);

                return MovementFSM.accelerate;
            }

            return MovementFSM.decelerate;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;

            speedToReduce = player.rigidBody.linearVelocityX;

            speedReductionPerFrame = speedToReduce / decelerationFrames;
        }

        public override void Update()
        {
            if (currentFrame >= decelerationFrames) parentFSM.ChangeState(MovementFSM.idle);

            if (player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle &&
                player.GetFSM(player.attaFSMIdx).currentState == AttackFSM.idle)
                player.SetSpeedX(speedReductionPerFrame * (decelerationFrames - currentFrame));
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