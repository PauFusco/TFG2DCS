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

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            return DashFSM.dash;
        }

        public override void OnEnter()
        {
            player.SetSpeedY(.0f);
            player.invulnerable = true;
            player.rigidBody.gravityScale = .0f;

            prevSpeed = player.rigidBody.linearVelocityX;
         
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= activeFrames)
            {
                parentFSM.ChangeState(DashFSM.idle);
            }

            if (player.rigidBody.gravityScale != .0f)
            {
                player.rigidBody.gravityScale = .0f;
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

            // Set X speed to 0?
            player.rigidBody.linearVelocityX = prevSpeed;
        }
    }
}