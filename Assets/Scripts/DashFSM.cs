namespace PFSM
{
    public class DashFSM : PlayerFSM
    {
        public static DIdleState idle;
        public static DashState dash;

        public DashFSM(PlayerBehaviour player, float dashCooldown, float dashDuration)
        {
            this.player = player;

            idle = new(this, player, dashCooldown);
            dash = new(this, player, dashDuration);

            currentState = idle;
        }

        public override void Update()
        {
            currentState.Update();

            if (currentState == dash &&
                dash.currentDashDuration >= dash.fullDashDuration)
            {
                ChangeState(idle);
            }
        }
    }
}