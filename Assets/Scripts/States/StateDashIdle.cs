using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DIdleState : BaseState
    {
        private float dashCD;
        private readonly float baseDashCD;

        public DIdleState(PlayerBehaviour player, float baseDashCooldown) : base(player)
        {
            baseDashCD = baseDashCooldown;
            dashCD = .0f;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (dashCD >= baseDashCD &&
                ctx.action.name == "Dash" &&
                ctx.action.inProgress)
            {
                return DashFSM.dash;
            }

            return DashFSM.idle;
        }

        public override void OnEnter()
        {
            dashCD = .0f;
        }

        public override void Update()
        {
            dashCD += Time.deltaTime;
        }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}