using UnityEngine.InputSystem;

namespace PFSM
{
    public abstract class BaseState
    {
        public PlayerBehaviour player;

        public BaseState(PlayerBehaviour player)
        {
            this.player = player;
        }

        public abstract BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx);
        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }
}