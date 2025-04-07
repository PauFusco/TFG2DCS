using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class BaseState
    {
        public PlayerBehaviour player;
        protected PlayerFSM parentFSM;

        public BaseState(PlayerFSM parentFSM, PlayerBehaviour player)
        {
            this.parentFSM = parentFSM;
            this.player = player;
        }

        public abstract BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx);

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }
}