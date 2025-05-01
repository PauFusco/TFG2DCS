using UnityEngine.InputSystem;

namespace PFSM
{
    public class AttackFSM : PlayerFSM
    {
        public static AIdleState idle;

        public AttackFSM(PlayerBehaviour player)
        {
            this.player = player;

            idle = new(this, player);

            currentState = idle;
        }
    }
}