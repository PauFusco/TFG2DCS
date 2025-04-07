using UnityEngine.InputSystem;
using UnityEngine;

namespace PFSM
{
    public class GroundState : BaseState
    {
        public GroundState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Jump" && ctx.action.inProgress)
            {
                player.Jump();
                player.jumping = true;
                return JumpFSM.airState;
            }

            return JumpFSM.groundState;
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