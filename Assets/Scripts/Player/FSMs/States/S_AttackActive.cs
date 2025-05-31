using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class ActiveState : BaseState
    {
        private PAttack.Attack currentAttack;

        private float chargeMultiplier;
        private float currentFrame;

        public ActiveState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        {
            currentFrame = .0f;
        }

        public override BaseState HandleInput(InputState input)
        {
            if (player.GetFSM(PlayerFSMControl.dashFSMIdx).currentState != DashFSM.idle) return AttackFSM.idle;
            else return AttackFSM.active;
        }

        public void SetData(PAttack.Attack attack, float chargeMultiplier)
        {
            this.chargeMultiplier = chargeMultiplier;
            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.red);

            player.EnableCurrentAttackCollider(((AttackFSM)parentFSM).currentAttack);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= currentAttack.active)
            {
                AttackFSM.recovery.SetData(currentAttack, chargeMultiplier);
                parentFSM.ChangeState(AttackFSM.recovery);
            }
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        {
            player.DisableCurrentAttackCollider(((AttackFSM)parentFSM).currentAttack);
        }
    }
}