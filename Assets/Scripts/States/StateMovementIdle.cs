using UnityEngine.InputSystem;

namespace PFSM
{
    public class MIdleState : BaseState
    {
        public MIdleState(PlayerBehaviour player) : base(player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
                return MovementFSM.walk;

            return MovementFSM.idle;
        }

        public override void OnEnter()
        { }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}