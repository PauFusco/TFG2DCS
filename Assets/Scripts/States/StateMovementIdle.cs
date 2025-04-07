using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class MIdleState : BaseState
    {
        public MIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        {
            thisState = State.IDLE;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                float speedMult = ctx.ReadValue<Vector2>().x;

                player.lookDirection = speedMult >= 0;

                MovementFSM.accelerate.targetSpeed = speedMult;

                MovementFSM.accelerate.direction =
                    player.lookDirection ? 1.0f : -1.0f;
                
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

        }
    }
}