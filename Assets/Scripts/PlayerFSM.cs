using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class PlayerFSM
    {
        public BaseState currentState;

        public virtual void HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            BaseState checkState = currentState.HandleInput(player, ctx);

            if (currentState != checkState) ChangeState(checkState);
        }

        public virtual void Update()
        {
            currentState.Update();
        }

        public virtual void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        protected virtual void ChangeState(BaseState state)
        {
            var prevState = currentState;
            var postState = state;

            prevState.OnExit();
            postState.OnEnter();

            currentState = state;
        }
    }
}