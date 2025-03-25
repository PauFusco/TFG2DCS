using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class MovementFSM
    {
        public BaseState currentState;

        public static WalkState walk;
        public static IdleState idle;

        public MovementFSM(PlayerBehaviour player)
        {
            walk = new(player);
            idle = new(player);

            currentState = idle;
        }

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public void HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            BaseState checkState = currentState.HandleInput(player, ctx);

            if (currentState != checkState) ChangeState(checkState);
        }

        public void ChangeState(BaseState state)
        {
            var prevState = currentState;
            var postState = state;

            prevState.OnExit();
            postState.OnEnter();

            currentState = state;
        }
    }
}