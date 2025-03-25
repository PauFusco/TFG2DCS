using UnityEngine.InputSystem;

namespace PFSM
{
    public class WalkState : BaseMovementState
    {
        public WalkState()
        { }

        public override void HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                player.Move();
            }
        }
    }
}