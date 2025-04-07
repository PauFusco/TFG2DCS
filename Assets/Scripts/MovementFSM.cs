namespace PFSM
{
    public class MovementFSM : PlayerFSM
    {
        public static MIdleState idle;
        public static WalkState walk;
        public static AccelerateState accelerate;
        public static DecelerateState decelerate;
        public static TurnState turn;

        public MovementFSM(PlayerBehaviour player, float maxSpeed, float turnFrames, float accFrames, float decFrames)
        {
            idle = new(this, player);
            walk = new(this, player, maxSpeed);
            accelerate = new(this, player, maxSpeed, accFrames);
            decelerate = new(this, player, maxSpeed, decFrames);
            turn = new(this, player, maxSpeed, turnFrames);

            currentState = idle;
        }

        public override void Update()
        {
            currentState.Update();
        }
    }
}