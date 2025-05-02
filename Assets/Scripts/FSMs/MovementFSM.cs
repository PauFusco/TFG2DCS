namespace PFSM
{
    public class MovementFSM : PlayerFSM
    {
        public static MIdleState idle;
        public static WalkState walk;
        public static AccelerateState accelerate;
        public static DecelerateState decelerate;

        public MovementFSM(
            PlayerBehaviour player,
            float maxSpeed,
            float accFrames,
            float decFrames)
        {
            idle = new(this, player);
            walk = new(this, player, maxSpeed);
            accelerate = new(this, player, maxSpeed, accFrames);
            decelerate = new(this, player, maxSpeed, decFrames);

            currentState = idle;
        }
    }
}