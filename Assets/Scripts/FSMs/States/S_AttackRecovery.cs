using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class RecoveryState : BaseState
    {
        private PAttack.Attack currentAttack;
        private float currentFrame;

        public RecoveryState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        {
            currentFrame = .0f;
        }

        public override BaseState HandleInput(InputState input)
        {
            if (!currentAttack.cancellable) return AttackFSM.recovery;
            else
            {
                foreach (var attack in currentAttack.cancellableInto)
                {
                    if (input.Compare(attack.input, KeyState.DOWN))
                    {
                        if (attack.chargeable)
                        {
                            AttackFSM.charge.SetData(attack);
                            return AttackFSM.charge;
                        }
                        else
                        {
                            AttackFSM.anticipation.SetData(attack);
                            return AttackFSM.anticipation;
                        }
                    }
                }

                return AttackFSM.recovery;
            }
        }

        public void SetData(PAttack.Attack attack)
        {
            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.blue);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if(currentFrame>= currentAttack.recovery)
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