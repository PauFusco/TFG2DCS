using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class AnticipationState : BaseState
    {
        private PAttack.Attack currentAttack;

        private float chargeMultiplier;
        private float currentFrame;

        public AnticipationState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        { }

        public override BaseState HandleInput(InputState input)
        {
            if (player.GetFSM(PlayerFSMControl.dashFSMIdx).currentState != DashFSM.idle) return AttackFSM.idle;
            else return AttackFSM.anticipation;
        }

        public void SetData(PAttack.Attack attack, float chargeMultiplier)
        {
            currentAttack = attack;
            this.chargeMultiplier = chargeMultiplier;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.green);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= currentAttack.anticipation * chargeMultiplier)
            {
                AttackFSM.active.SetData(currentAttack, chargeMultiplier);
                parentFSM.ChangeState(AttackFSM.active);
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