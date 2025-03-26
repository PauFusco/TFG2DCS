using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class PlayerFSM
    {
        public BaseState currentState;

        protected virtual BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContrext ctx)
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

    public class MovementFSM : PlayerFSM
    {
        protected static IdleState idle;
        protected static WalkState walk;

        public MovementFSM(PlayerBehaviour player)
        {
            idle = new(player);
            walk = new(player);

            currentState = idle;
        }
    }

    public class JumpFSM : PlayerFSM
    {
        protected AirState airState;
        protected GroundState groundState;
        
        public JumpFSM(PlayerBehaviour player)
        {
            airState = new();
            groundState = new();

            currentState = groundState;
        }

    }
}