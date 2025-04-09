using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DecelerateState : BaseState
    {
        private readonly float decelerationFrames;
        private readonly float maxSpeed;
        private readonly float maxAirSpeed;

        private float currentFrame;
        private float speedReductionPerFrame;

        public float direction;
        public float speedToReduce;

        public DecelerateState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed,
            float maxAirSpeed,
            float decFrames)
            : base(parentFSM, player)
        {
            decelerationFrames = decFrames;
            this.maxSpeed = maxSpeed;
            this.maxAirSpeed = maxAirSpeed;
            
            currentFrame = .0f;

            thisMoveState = MoveStateE.DECELERATE;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                if (!ctx.action.inProgress) return MovementFSM.decelerate;

                float speedMult = ctx.ReadValue<Vector2>().x;
                bool speedMultiplierDirection = speedMult >= 0;

                if (speedMultiplierDirection != player.lookDirection)
                {
                    player.lookDirection = speedMultiplierDirection;

                    MovementFSM.turn.targetSpeed = speedMult * maxSpeed;

                    MovementFSM.turn.targetDirection =
                        player.lookDirection ? 1.0f : -1.0f;

                    return MovementFSM.turn;
                }
                else
                {
                    player.lookDirection = speedMult >= 0;

                    MovementFSM.accelerate.targetSpeed = speedMult * maxSpeed;

                    MovementFSM.accelerate.direction =
                        player.lookDirection ? 1.0f : -1.0f;

                    return MovementFSM.accelerate;
                }

            }

            return MovementFSM.decelerate;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;

            speedToReduce = player.rigidBody.linearVelocityX;
            direction = player.lookDirection ? 1.0f : -1.0f;

            speedReductionPerFrame = speedToReduce / decelerationFrames;
        }

        public override void Update()
        {
            if (currentFrame >= decelerationFrames)
            {
                parentFSM.ChangeState(MovementFSM.idle);
            }

            if (player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle)
                player.SetSpeedX(speedReductionPerFrame * (decelerationFrames - currentFrame));
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }

    }
}