using CustomInputControl;
using System.Diagnostics;
using System.Transactions;

namespace PFSM
{
    public class AttackFSM : PlayerFSM
    {
        public static PAttack.Attack[] attacks;

        public static AIdleState idle;
        public static ChargeState charge;
        public static AnticipationState anticipation;
        public static ActiveState active;
        public static RecoveryState recovery;

        public AttackFSM(
            PlayerBehaviour player,
            PAttack.Attack[] attacks,
            float chargeFrames)
        {
            this.player = player;
            AttackFSM.attacks = attacks;

            idle = new(this, player);
            charge = new(this, player, chargeFrames);
            anticipation = new(this, player);
            active = new(this, player);
            recovery = new(this, player);

            currentState = idle;
        }
    }
}