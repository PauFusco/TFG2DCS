using PFSM;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PFSM
{
    public enum MoveStateE
    {
        IDLE, ACCELERATE, WALK, DECELERATE, TURN, Default
    }

    public enum JumpStateE
    {
        GROUND, JUMP, FREEFALL, Default
    }

    public abstract class PlayerFSM
    {
        public BaseState currentState;

        protected PlayerBehaviour player;

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

        public virtual void ChangeState(BaseState state)
        {
            var prevState = currentState;
            var postState = state;

            prevState.OnExit();
            postState.OnEnter();

            currentState = state;
        }
    }
}