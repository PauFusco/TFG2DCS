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
                foreach (var attack in AttackFSM.attacks)
                {
                    if (input.Compare(attack.input, player.airborne))
                    {
                        if (attack.chargeable)
                        {
                            AttackFSM.charge.SetData(attack);
                            return AttackFSM.charge;
                        }
                        else
                        {
                            AttackFSM.anticipation.SetData(attack, 1.0f);
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
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}