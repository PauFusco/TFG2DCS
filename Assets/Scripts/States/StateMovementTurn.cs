using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class TurnState : BaseState
    {
        private readonly float maxSpeed;
        private readonly float maxAirSpeed;
        private readonly float turnFrames;

        private float currentFrame;
        private float currentSpeed;
        private float speedInterval;

        public float targetDirection;
        public float targetSpeed;

        public TurnState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed,
            float maxAirSpeed,
            float turnFrames)
            : base(parentFSM, player)
        {
            this.maxSpeed = maxSpeed;
            this.maxAirSpeed = maxAirSpeed;
            this.turnFrames = turnFrames;
            
            currentFrame = .0f;

            thisMoveState = MoveStateE.TURN;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                if (!ctx.action.inProgress) return MovementFSM.decelerate;

                float speedMultiplier = ctx.ReadValue<Vector2>().x;
                targetSpeed = speedMult * direction * 
                    player.grounded ? maxSpeed : maxAirSpeed;
            }

            return MovementFSM.turn;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;
            currentSpeed = player.rigidBody.linearVelocity.x;
            speedInterval = Mathf.Abs(targetSpeed - currentSpeed);
            player.lookDirection = targetSpeed >= 0;
        }

        public override void Update()
        {
            if (currentFrame >= turnFrames)
            {
                MovementFSM.walk.speed = targetSpeed;
                MovementFSM.walk.direction = targetDirection;

                parentFSM.ChangeState(MovementFSM.walk);
            }

            if (player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle)
                player.SetSpeedX(currentSpeed);
        }

        public override void FixedUpdate()
        {
            currentFrame++;
            currentSpeed += speedInterval / turnFrames * targetDirection;
        }

        public override void OnExit()
        { }


    }
}