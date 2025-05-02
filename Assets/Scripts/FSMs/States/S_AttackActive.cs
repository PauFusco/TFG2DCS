using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class ActiveState : BaseState
    {
        private PAttack.Attack currentAttack;
        private float currentFrame;

        public ActiveState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        {
            currentFrame = .0f;
        }

        public override BaseState HandleInput(InputState input)
        {
            return AttackFSM.active;
        }

        public void SetData(PAttack.Attack attack)
        {
            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.red);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= currentAttack.active)
            {
                AttackFSM.recovery.SetData(currentAttack);
                parentFSM.ChangeState(AttackFSM.recovery);
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