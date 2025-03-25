using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public interface IState
    {
        void HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx);

        void OnEnter();

        void Update();

        void OnExit();
    }
}