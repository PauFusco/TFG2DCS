using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class AccelerateState : BaseState
    {
        private readonly float maxSpeed;
        private readonly float maxAirSpeed;
        private readonly float accelerationFrames;

        private float currentFrame;

        public float direction;
        public float targetSpeed;

        public AccelerateState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed,
            float maxAirSpeed,
            float accelerationFrames)
            : base(parentFSM, player)
        {
            this.maxSpeed = maxSpeed;
            this.maxAirSpeed = maxAirSpeed;
            this.accelerationFrames = accelerationFrames;
            
            currentFrame = .0f;

            thisMoveState = MoveStateE.ACCELERATE;
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
                    targetSpeed = speedMult * direction * 
                        player.grounded ? maxSpeed : maxAirSpeed;                    
                }
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
            if (currentFrame >= accelerationFrames)
            {
                MovementFSM.walk.speed = targetSpeed;
                MovementFSM.walk.direction = direction;

                parentFSM.ChangeState(MovementFSM.walk);
            }

            if (player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle)
                player.SetSpeedX(targetSpeed / accelerationFrames * currentFrame * direction);
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }
    }
}