namespace PFSM
{
    public class JumpFSM : PlayerFSM
    {
        public static GroundState ground;
        public static JumpState jump;
        public static FreeFallState freeFall;

        public JumpFSM(
            PlayerBehaviour player,
            float speed,
            float height,
            float cutoffFrames,
            float baseGravityMultiplier,
            float fallGravityMultiplier)
        {
            this.player = player;

            ground = new(this, player);
            jump = new(this, player, speed, height, cutoffFrames);
            freeFall = new(this, player, baseGravityMultiplier, fallGravityMultiplier);

            currentState = ground;
        }
    }
}