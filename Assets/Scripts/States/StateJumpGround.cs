using UnityEngine.InputSystem;

namespace PFSM
{
    public class GroundState : BaseState
    {
        public GroundState(PlayerBehaviour player) : base(player)
        { }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Jump" && ctx.action.IsPressed())
            {
                player.Jump();

                return JumpFSM.airState;
            }

            return JumpFSM.groundState;
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