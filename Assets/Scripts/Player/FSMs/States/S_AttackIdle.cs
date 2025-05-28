using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class AIdleState : BaseState
    {
        public AIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        { }

        public override BaseState HandleInput(InputState input)
        {
            if (player.GetFSM(player.dashFSMIdx).currentState != DashFSM.idle ||
                player.GetFSM(player.jumpFSMIdx).currentState != JumpFSM.ground) return AttackFSM.idle;
            else
            {
                for (int i = 0; i < AttackFSM.attacks.Length; i++)
                {
                    if (input.Compare(AttackFSM.attacks[i].input, player.airborne))
                    {
                        if (AttackFSM.attacks[i].chargeable)
                        {
                            AttackFSM.charge.SetData(AttackFSM.attacks[i]);
                            ((AttackFSM)parentFSM).currentAttack = i;
                            return AttackFSM.charge;
                        }
                        else
                        {
                            AttackFSM.anticipation.SetData(AttackFSM.attacks[i], 1.0f);
                            ((AttackFSM)parentFSM).currentAttack = i;
                            return AttackFSM.anticipation;
                        }
                    }
                }

                return AttackFSM.idle;
            }
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.white);
            ((AttackFSM)parentFSM).currentAttack = -1;
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        {
            Debug.Log(((AttackFSM)parentFSM).currentAttack);
        }
    }
}