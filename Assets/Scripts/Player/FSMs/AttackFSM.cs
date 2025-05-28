namespace PFSM
{
    public class AttackFSM : PlayerFSM
    {
        public static PAttack.Attack[] attacks;
        public int currentAttack;

        public static AIdleState idle;
        public static ChargeState charge;
        public static AnticipationState anticipation;
        public static ActiveState active;
        public static RecoveryState recovery;

        public AttackFSM(
            PlayerBehaviour player,
            PAttack.Attack[] attacks,
            float minChargeFrames,
            float effectiveChargeFrames,
            float maxChargeFrames)
        {
            this.player = player;
            AttackFSM.attacks = attacks;

            idle = new(this, player);
            charge = new(this, player, minChargeFrames, effectiveChargeFrames, maxChargeFrames);
            anticipation = new(this, player);
            active = new(this, player);
            recovery = new(this, player);

            currentState = idle;
            currentAttack = -1;
        }
    }
}