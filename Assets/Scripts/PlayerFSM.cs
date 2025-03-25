using UnityEngine.InputSystem;

namespace PFSM
{
    public class MovementFSM
    {
        public BaseMovementState currentState;

        public static WalkState walk;

        public MovementFSM()
        {
            walk = new();
            currentState = walk;
        }

        public void HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            currentState.HandleInput(player, ctx);
        }

        public void ChangeState(BaseMovementState state)
        {
            var prevState = currentState;
            var postState = state;

            prevState.OnExit();
            postState.OnEnter();

            currentState = state;
        }
    }
}