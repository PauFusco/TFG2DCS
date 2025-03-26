namespace PFSM
{
    public class MovementFSM : PlayerFSM
    {
        public static IdleState idle;
        public static WalkState walk;

        public MovementFSM(PlayerBehaviour player)
        {
            idle = new(player);
            walk = new(player);

            currentState = idle;
        }
    }
}