using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class AccelerateState : BaseState
    {
        private readonly float maxSpeed;
        private readonly float accelerationFrames;

        private float currentFrame;

        public float direction;
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

            currentFrame = .0f;
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
                    else targetSpeed = speedMultiplier * maxSpeed * direction;
                }
                else return MovementFSM.decelerate;
            }

            return MovementFSM.accelerate;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;
            player.lookDirection = targetSpeed >= 0;
        }

        public override void Update()
        {
            player.SetSpeedX(targetSpeed / accelerationFrames * currentFrame * direction);

            if (currentFrame >= accelerationFrames)
            {
                MovementFSM.walk.speed = targetSpeed;
                MovementFSM.walk.direction = direction;

                parentFSM.ChangeState(MovementFSM.walk);
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