using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class MIdleState : BaseState
    {
        private float speedMultiplier;

        public MIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                speedMultiplier = ctx.ReadValue<Vector2>().x;
                return MovementFSM.accelerate;
            }
            return MovementFSM.idle;
        }

        public override void OnEnter()
        { }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        {
            player.lookDirection = speedMultiplier >= 0;

            MovementFSM.accelerate.targetSpeed = speedMultiplier;

            MovementFSM.accelerate.direction =
                player.lookDirection ? 1.0f : -1.0f;
        }
    }
}