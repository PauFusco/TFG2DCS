using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class WalkState : BaseState
    {
        private Vector2 move;

        public WalkState(PlayerBehaviour player) : base(player)
        {
            move = new();
        }

        public override BaseState HandleInput(PlayerBehaviour player, InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<Vector2>();
            Vector2 notmove = new(0, 0);

            if (move == notmove)
                return MovementFSM.idle;

            return MovementFSM.walk;
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            player.Move(move);
        }
    }
}