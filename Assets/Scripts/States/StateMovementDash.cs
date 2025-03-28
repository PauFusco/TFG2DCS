using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class DashState : BaseState
    {
        private float baseDashDuration;
        public float dashState;

        public DashState(PlayerBehaviour player) : base(player)
        {
            baseDashDuration = 1.0f;
            dashState = baseDashDuration;
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            return MovementFSM.dash;
        }

        public override void OnEnter()
        { }

        public override void Update()
        {
            dashState -= Time.deltaTime;
        }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        {
            dashState = baseDashDuration;
        }
    }
}