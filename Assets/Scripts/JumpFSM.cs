namespace PFSM
{
    public class JumpFSM : PlayerFSM
    {
        public static AirState airState;
        public static GroundState groundState;

        public JumpFSM(PlayerBehaviour player, float fallGravityMultiplier)
        {
            this.player = player;

            airState = new(player, fallGravityMultiplier);
            groundState = new(player);

            currentState = groundState;
        }

        public override void Update()
        {
            currentState.Update();

            if (currentState == airState)
            {
                if (player.rigidBody.linearVelocityY == 0)
                    ChangeState(groundState);
            }
            else if (currentState == groundState)
            {
                if (player.rigidBody.linearVelocityY != 0)
                    ChangeState(airState);
            }
        }
    }
}