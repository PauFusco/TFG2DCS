using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DashState : BaseState
    {
        private readonly float speed;
        private readonly float activeFrames;

        private float currentFrame;

        private float prevSpeed;


        public DashState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float speed,
            float activeFrames)
            : base(parentFSM, player)
        {
            this.speed = speed;
            this.activeFrames = activeFrames;

            currentFrame = .0f;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            return DashFSM.dash;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;
            player.invulnerable = true;

            prevSpeed = player.rigidBody.linearVelocityX;
        }

        public override void Update()
        {
            if (currentFrame >= activeFrames)
            {
                parentFSM.ChangeState(DashFSM.idle);
            }
        }

        public override void FixedUpdate()
        {
            float dashDirection = player.lookDirection ? 1.0f : -1.0f;
            player.Dash(speed * dashDirection);

            currentFrame++;
        }

        public override void OnExit()
        {
            player.invulnerable = false;

            player.rigidBody.linearVelocityX = prevSpeed;
        }
    }
}