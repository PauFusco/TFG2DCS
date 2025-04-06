namespace PFSM
{
    public class MovementFSM : PlayerFSM
    {
        public static MIdleState idle;
        public static WalkState walk;

        public MovementFSM(PlayerBehaviour player)
        {
            idle = new(player);
            walk = new(player);

            currentState = idle;
        }

        public override void Update()
        {
            currentState.Update();
        }
    }
}