using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DashState : BaseState
    {
        private readonly float baseDashDuration;
        public float dashState;

        public DashState(PlayerBehaviour player, float baseDashDuration) : base(player)
        {
            this.baseDashDuration = baseDashDuration;
            dashState = this.baseDashDuration;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            return DashFSM.dash;
        }

        public override void OnEnter()
        {
            player.invulnerable = true;
        }

        public override void Update()
        {
            dashState -= Time.deltaTime;
        }

        public override void FixedUpdate()
        {
            player.Dash();
        }

        public override void OnExit()
        {
            dashState = baseDashDuration;
            player.invulnerable = false;
            player.Move(new(0, 0));
        }
    }
}