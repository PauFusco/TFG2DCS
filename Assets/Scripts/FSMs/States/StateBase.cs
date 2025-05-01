using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class BaseState
    {
        protected PlayerFSM parentFSM;

        public MoveStateE thisMoveState;
        public JumpStateE thisJumpState;
        public PlayerBehaviour player;

        public BaseState(PlayerFSM parentFSM, PlayerBehaviour player)
        {
            this.parentFSM = parentFSM;
            this.player = player;
            thisMoveState = MoveStateE.Default;
            thisJumpState = JumpStateE.Default;
        }

        public abstract BaseState HandleInput(InputAction.CallbackContext ctx);

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }
}