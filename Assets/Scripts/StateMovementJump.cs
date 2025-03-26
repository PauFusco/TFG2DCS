using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class AirState : BaseState
    {
        public AirState(PlayerBehaviour player) : base(player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        { }

        public override void OnEnter()
        {
            player.grounded = false;
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        {
            if(player.rigidBody.linearVelocity.y == 0)
            {
                // Change State to ground
            }
        }

        public override void OnExit()
        { }
    }

    public class GroundState : BaseState
    {
        public GroundState(PlayerBehaviour player) : base(player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if(ctx.action.name == "Jump")
            {
                player.jump;

                return JumpFSM.airState;
            }

            return JumpFSM.groundState;
        }

        public override void OnEnter()
        {
            player.grounded = true;
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }

    }
}