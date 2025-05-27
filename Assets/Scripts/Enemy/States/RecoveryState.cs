using UnityEngine;

namespace EFSM
{
    public class RecoveryState : BaseState
    {
        private readonly float recoveryFrames;

        private float currentFrame;

        public RecoveryState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            float recoveryFrames)
            : base(parentFSM, enemy)
        {
            this.recoveryFrames = recoveryFrames;

            currentFrame = .0f;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.blue);
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= recoveryFrames)
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
