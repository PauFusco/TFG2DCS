namespace PFSM
{
    public class DashFSM : PlayerFSM
    {
        public static DIdleState idle;
        public static DashState dash;

        public DashFSM(PlayerBehaviour player, float dashSpeed, float dashCooldownFrames, float dashFrames)
        {
            this.player = player;

            idle = new(this, player, dashCooldownFrames);
            dash = new(this, player, dashSpeed, dashFrames);

            currentState = idle;
        }
    }
}