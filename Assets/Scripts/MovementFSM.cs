using UnityEngine;
using UnityEngine.InputSystem;

namespace PFSM
{
    public class MovementFSM : PlayerFSM
    {
        public static IdleState idle;
        public static WalkState walk;
        public static DashState dash;

        public MovementFSM(PlayerBehaviour player)
        {
            idle = new(player);
            walk = new(player);
            dash = new(player);

            currentState = idle;
        }

        public override void Update()
        {
            if (dash.dashState <= 0.0f)
            {
                ChangeState(idle);
            }
        }
    }
}