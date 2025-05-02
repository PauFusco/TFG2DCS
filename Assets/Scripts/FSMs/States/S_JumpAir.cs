using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class JumpState : BaseState
    {
        private readonly float speed;
        private readonly float maxHeight;
        private readonly float cutoffFrames;

        private float targetHeight;
        private float originalHeight;

        private float currentFrame;

        private bool heightReached;
        private bool startFreeFall;

        public JumpState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float speed,
            float maxHeight,
            float cutoffFrames)
            : base(parentFSM, player)
        {
            this.speed = speed;
            this.maxHeight = maxHeight;
            this.cutoffFrames = cutoffFrames;

            currentFrame = .0f;
            heightReached = false;

            thisJumpState = JumpStateE.JUMP;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            if (input.jump == CustomInputControl.KeyState.UP)
            {
                if (currentFrame < cutoffFrames)
                {
                    startFreeFall = true;
                }
                else
                    return JumpFSM.freeFall;
            }

            return JumpFSM.jump;
        }

        public override void OnEnter()
        {
            originalHeight = player.transform.position.y;
            targetHeight = originalHeight + maxHeight;

            heightReached = false;
            startFreeFall = false;

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (player.grounded)
            {
                parentFSM.ChangeState(JumpFSM.ground);
            }

            if ((startFreeFall &&
                currentFrame >= cutoffFrames) ||
                player.invulnerable)
            {
                parentFSM.ChangeState(JumpFSM.freeFall);
            }

            if (player.transform.position.y >= targetHeight)
            {
                heightReached = true;
            }


        }

        public override void FixedUpdate()
        {
            currentFrame++;

            if ((player.transform.position.y < targetHeight ||
                currentFrame < cutoffFrames) &&
                !heightReached)
            {
                player.SetSpeedY(speed);
            }
        }

        public override void OnExit()
        { }
    }
}