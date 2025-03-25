using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class BaseMovementState : IState
    {
        public virtual void HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        { }

        public virtual void OnEnter()
        { }

        public virtual void OnExit()
        { }

        public virtual void Update()
        { }
    }
}