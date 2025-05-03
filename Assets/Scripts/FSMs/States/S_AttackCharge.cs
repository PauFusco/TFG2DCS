using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class ChargeState : BaseState
    {
        public PAttack.Attack currentAttack;

        private float minChargeFrames;
        private float effectiveChargeFrames;
        private float maxChargeFrames;
        private bool activateAttackNextFrame;
        private float currentFrame;

        public ChargeState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float minChargeFrames,
            float effectiveChargeFrames,
            float maxChargeFrames)
            : base(parentFSM, player)
        {
            this.minChargeFrames = minChargeFrames;
            this.effectiveChargeFrames = effectiveChargeFrames;
            this.maxChargeFrames = maxChargeFrames;

            activateAttackNextFrame = false;
            currentFrame = .0f;
        }

        public override BaseState HandleInput(InputState input)
        {
            if (player.GetFSM(player.dashFSMIdx).currentState != DashFSM.idle ||
                player.GetFSM(player.jumpFSMIdx).currentState != JumpFSM.ground) return AttackFSM.idle;
            else if (!input.Compare(currentAttack.input, KeyState.REPEAT, player.airborne))
            {
                if (currentFrame < minChargeFrames)
                {
                    activateAttackNextFrame = true;
                    return AttackFSM.charge;
                }
                else
                {
                    float chargeMult = currentFrame / effectiveChargeFrames >= 1.0f ?
                        1.0f :
                        currentFrame / effectiveChargeFrames;

                    AttackFSM.anticipation.SetData(currentAttack, chargeMult);
                    return AttackFSM.anticipation;
                }
            }
            else return AttackFSM.charge;
        }

        public void SetData(PAttack.Attack attack)
        {
            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.magenta);

            activateAttackNextFrame = false;
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= maxChargeFrames ||
               (activateAttackNextFrame && currentFrame >= minChargeFrames))
            {
                float chargeMult = currentFrame / effectiveChargeFrames >= 1.0f ?
                    1.0f :
                    currentFrame / effectiveChargeFrames;

                AttackFSM.anticipation.SetData(currentAttack, chargeMult);
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