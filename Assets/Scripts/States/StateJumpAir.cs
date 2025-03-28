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
        {
            if (player.rigidBody.linearVelocityY > 0) player.rigidBody.gravityScale = 1;
            else player.rigidBody.gravityScale = player.jFallGravityMultiplier;
        }

        public override void OnExit()
        {
            if (player.rigidBody.linearVelocityY <= 0) player.rigidBody.gravityScale = 1;
        }
    }
}