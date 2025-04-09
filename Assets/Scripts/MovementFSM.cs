namespace PFSM
{
    public class MovementFSM : PlayerFSM
    {
        public static MIdleState idle;
        public static WalkState walk;
        public static AccelerateState accelerate;
        public static DecelerateState decelerate;
        public static TurnState turn;

        public MovementFSM(
            PlayerBehaviour player,
            float maxSpeed,
            float maxAirSpeed,
            float turnFrames,
            float accFrames,
            float decFrames)
        {
            idle = new(this, player);
            walk = new(this, player, maxSpeed, maxAirSpeed);
            accelerate = new(this, player, maxSpeed, maxAirSpeed, accFrames);
            decelerate = new(this, player, maxSpeed, maxAirSpeed, decFrames);
            turn = new(this, player, maxSpeed, maxAirSpeed, turnFrames);

            currentState = idle;
        }
    }
}