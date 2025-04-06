using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class AirState : BaseState
    {
        private float fallGravMult;

        public AirState(PlayerBehaviour player, float fallGravityMultiplier) : base(player)
        {
            this.fallGravMult = fallGravityMultiplier;
        }

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
            else player.rigidBody.gravityScale = fallGravMult;
        }

        public override void OnExit()
        {
            if (player.rigidBody.linearVelocityY <= 0) player.rigidBody.gravityScale = 1;
        }
    }
}