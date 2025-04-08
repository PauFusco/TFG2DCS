using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class WalkState : BaseState
    {
        private readonly float maxSpeed;

        public float speed;
        public float direction;

        public WalkState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed)
            : base(parentFSM, player)
        {
            this.maxSpeed = maxSpeed;
            thisState = State.WALK;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                if (!ctx.action.inProgress) return MovementFSM.decelerate;

                float speedMult = ctx.ReadValue<Vector2>().x;
                speed = speedMult * maxSpeed * direction;

                bool speedMultiplierDirection = speedMult >= 0;

                Debug.Log(speedMult);

                if (speedMultiplierDirection != player.lookDirection)
                {
                    player.lookDirection = speedMultiplierDirection;

                    MovementFSM.turn.targetSpeed = speed;

                    MovementFSM.turn.targetDirection =
                        player.lookDirection ? 1.0f : -1.0f;

                    return MovementFSM.turn;
                }
            }

            return MovementFSM.walk;
        }

        public override void OnEnter()
        { }

        public override void Update()
        {
            if (player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle)
                player.SetSpeedX(speed * direction);
        }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}