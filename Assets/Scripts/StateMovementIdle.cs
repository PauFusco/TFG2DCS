using UnityEngine.InputSystem;

namespace PFSM
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerBehaviour player) : base(player)
        {
        }

        public override void FixedUpdate()
        {
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
                return MovementFSM.walk;

            return MovementFSM.idle;
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
        }
    }
}