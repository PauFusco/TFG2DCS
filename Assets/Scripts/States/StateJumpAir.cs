using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class AirState : BaseState
    {
        private readonly float fallGravMult, jumpMaxDuration;
        private float jumpDuration;

        public AirState(PlayerBehaviour player, float fallGravityMultiplier, float jumpMaximumDuration) : base(player)
        {
            fallGravMult = fallGravityMultiplier;
            jumpMaxDuration = jumpMaximumDuration;

            jumpDuration = .0f;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Jump" && !ctx.action.inProgress)
            {
                player.SetSpeedY(0);
                player.jumping = false;
            }

            return JumpFSM.airState;
        }

        public override void OnEnter()
        {
            jumpDuration = .0f;
        }

        public override void Update()
        {
            jumpDuration += Time.deltaTime;
            if (jumpDuration >= jumpMaxDuration)
            {
                player.SetSpeedY(0);
                player.jumping = false;
            }
        }

        public override void FixedUpdate()
        {
            if (player.jumping) player.Jump();

            if (player.rigidBody.linearVelocityY > 0) player.rigidBody.gravityScale = 1;
            else player.rigidBody.gravityScale = fallGravMult;
        }

        public override void OnExit()
        {
            player.rigidBody.gravityScale = 1;
        }
    }
}