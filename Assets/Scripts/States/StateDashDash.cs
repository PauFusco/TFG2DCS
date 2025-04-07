using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DashState : BaseState
    {
        public readonly float fullDashDuration;
        public float currentDashDuration;

        public DashState(PlayerFSM parentFSM, PlayerBehaviour player, float dashMaximumDuration) : base(parentFSM, player)
        {
            fullDashDuration = dashMaximumDuration;
            currentDashDuration = .0f;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            return DashFSM.dash;
        }

        public override void OnEnter()
        {
            player.invulnerable = true;
            currentDashDuration = .0f;
        }

        public override void Update()
        {
            currentDashDuration += Time.deltaTime;
        }

        public override void FixedUpdate()
        {
            player.Dash();
        }

        public override void OnExit()
        {
            player.invulnerable = false;
            player.SetSpeedX(0);
        }
    }
}