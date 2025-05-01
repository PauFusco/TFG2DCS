using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DIdleState : BaseState
    {
        private readonly float cooldownFrames;

        private float currentFrame;

        public DIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float cooldownFrames)
            : base(parentFSM, player)
        {
            this.cooldownFrames = cooldownFrames;
            currentFrame = this.cooldownFrames;
        }

        public override BaseState HandleInput(InputAction.CallbackContext ctx)
        {
            if (currentFrame >= cooldownFrames &&
                ctx.action.name == "Dash" &&
                ctx.action.inProgress)
            {
                return DashFSM.dash;
            }

            return DashFSM.idle;
        }

        public override void OnEnter()
        {
            currentFrame = .0f;
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }
    }
}