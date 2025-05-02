using UnityEngine;
using CustomInputControl;

namespace PFSM
{
    public class AnticipationState : BaseState
    {
        private PAttack.Attack currentAttack;

        private float currentFrame;

        public AnticipationState(PlayerFSM parentFSM, PlayerBehaviour player) : base(parentFSM, player)
        { }

        public override BaseState HandleInput(InputState input)
        {
            return AttackFSM.anticipation;
        }

        public void SetData(PAttack.Attack attack)
        {
            currentAttack = attack;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.green);

            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= currentAttack.anticipation)
            {
                AttackFSM.active.SetData(currentAttack);
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