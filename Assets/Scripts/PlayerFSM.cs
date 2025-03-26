using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class PlayerFSM
    {
        public BaseState currentState;

        public virtual BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContrext ctx)
        {
            BaseState checkState = currentState.HandleInput(player, ctx);

            if (currentState != checkState) ChangeState(checkState);
        }
        public abstract void Update();
        public abstract void FixedUpdate();
        public virtual void ChangeState(BaseState state)
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
        protected static JumpState protectedjump;

        public MovementFSM(PlayerBehaviour player)
        {
            idle = new(player);
            walk = new(player);
            jump = new(player);

            currentState = idle;
        }

        public override void Update()
        {
            currentState.Update();
        }

        public override void FixedUpdate()
        {
            currentState.FixedUpdate();
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

    public class JumpFSM : PlayerFSM
    {
        protected AirState airState;
        protected GroundState groundState;
        
        public JumpFSM(PlayerBehaviour player)
        {
            currentState = groundState;
        }

    }
}