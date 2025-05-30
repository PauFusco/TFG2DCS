using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class RecoveryState : BaseState
    {
        private PAttack.Attack currentAttack;

        private float chargeMultiplier;
        private float currentFrame;

        public RecoveryState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        {
            currentFrame = .0f;
        }

        public override BaseState HandleInput(InputState input)
        {
            if (player.GetFSM(player.dashFSMIdx).currentState != DashFSM.idle) return AttackFSM.idle;
            else if (!currentAttack.cancellable) return AttackFSM.recovery;
            else
            {
                foreach (var cancellableIntoAttack in currentAttack.cancellableInto)
                {
                    if (input.Compare(cancellableIntoAttack.input, player.airborne))
                    {
                        for (int i = 0; i < AttackFSM.attacks.Length; i++)
                        {
                            if (AttackFSM.attacks[i] == cancellableIntoAttack)
                            {
                                ((AttackFSM)parentFSM).currentAttack = i;

                                if (cancellableIntoAttack.chargeable)
                                {
                                    AttackFSM.charge.SetData(cancellableIntoAttack);
                                    return AttackFSM.charge;
                                }
                                else
                                {
                                    AttackFSM.anticipation.SetData(cancellableIntoAttack, 1.0f);
                                    return AttackFSM.anticipation;
                                }
                            }
                        }
                    }
                }

                return AttackFSM.recovery;
            }
        }

        public void SetData(PAttack.Attack attack, float chargeMultiplier)
        {
            this.chargeMultiplier = chargeMultiplier;

            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.blue);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= currentAttack.recovery * chargeMultiplier)
            {
                parentFSM.ChangeState(AttackFSM.idle);
            }
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        { }
    }
}