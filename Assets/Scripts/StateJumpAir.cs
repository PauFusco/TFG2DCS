using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class AirState : BaseState
    {
        public AirState(PlayerBehaviour player) : base(player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            return JumpFSM.airState;
        }

        public override void OnEnter()
        { }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}