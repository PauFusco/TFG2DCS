using UnityEngine;

namespace EFSM
{
    public class AnticipationState : BaseState
    {
        private readonly float anticipationFrames;

        private float currentFrame;

        public AnticipationState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            float anticipationFrames)
            : base(parentFSM, enemy)
        {
            this.anticipationFrames = anticipationFrames;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.green);
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= anticipationFrames)
            {
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
