using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class ChargeState : BaseState
    {
        public PAttack.Attack currentAttack;

        public float chargeFrames;
        public float currentFrame;

        public ChargeState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float chargeFrames)
            : base(parentFSM, player)
        {
            this.chargeFrames = chargeFrames;

            currentFrame = .0f;
        }

        public override BaseState HandleInput(InputState input)
        {
            if (!input.Compare(currentAttack.input))
            {
                AttackFSM.anticipation.SetData(currentAttack);
                return AttackFSM.anticipation;
            }

            return AttackFSM.charge;
        }

        public void SetData(PAttack.Attack attack)
        {
            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.magenta);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= chargeFrames)
            {
                AttackFSM.anticipation.SetData(currentAttack);
                parentFSM.ChangeState(AttackFSM.anticipation);
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