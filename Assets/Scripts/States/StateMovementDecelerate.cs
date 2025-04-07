using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DecelerateState : BaseState
    {
        private readonly float decelerationFrames;
        private readonly float maxSpeed;

        private float currentFrame;
        private float speedReductionPerFrame;

        public float direction;
        public float speedToReduce;

        public DecelerateState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed,
            float decFrames)
            : base(parentFSM, player)
        {
            decelerationFrames = decFrames;
            this.maxSpeed = maxSpeed;
        }



        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                if (ctx.action.inProgress)
                {
                    float speedMultiplier = ctx.ReadValue<Vector2>().x;

                    bool speedMultiplierDirection = speedMultiplier >= 0;

                    if (speedMultiplierDirection != player.lookDirection)
                    {
                        player.lookDirection = speedMultiplierDirection;

                        MovementFSM.turn.targetSpeed = speedMultiplier * maxSpeed;

                        MovementFSM.turn.targetDirection =
                            player.lookDirection ? 1.0f : -1.0f;

                        return MovementFSM.turn;
                    }
                }
            }
            return MovementFSM.decelerate;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;

            speedReductionPerFrame = speedToReduce / decelerationFrames * direction;
        }

        public override void Update()
        {
            player.SetSpeedX(speedReductionPerFrame * (decelerationFrames - currentFrame));

            if (currentFrame >= decelerationFrames)
            {
                parentFSM.ChangeState(MovementFSM.idle);
            }
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }

    }
}