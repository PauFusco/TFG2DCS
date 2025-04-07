using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class BaseState
    {
        protected PlayerFSM parentFSM;

        public State thisState;
        public PlayerBehaviour player;

        public BaseState(PlayerFSM parentFSM, PlayerBehaviour player)
        {
            this.parentFSM = parentFSM;
            this.player = player;
            thisState = State.Default;
        }

        public abstract BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx);

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }
}