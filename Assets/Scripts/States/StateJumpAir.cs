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

            thisJumpState = JumpStateE.JUMP;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Jump" &&
                !ctx.action.inProgress)
            {
                return JumpFSM.freeFall;
            }

            return JumpFSM.jump;
        }

        public override void OnEnter()
        {
            originalHeight = player.transform.position.y;
            targetHeight = originalHeight + maxHeight;

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (player.grounded)
            {
                parentFSM.ChangeState(JumpFSM.ground);
            }
        }

        public override void FixedUpdate()
        {
            currentFrame++;

            if (player.transform.position.y < targetHeight ||
                currentFrame < cutoffFrames)
            {
                player.SetSpeedY(speed);
            }
        }

        public override void OnExit()
        { }
    }
}