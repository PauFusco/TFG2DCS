using UnityEngine;

namespace EFSM
{
    public abstract class BaseState
    {
        protected AttackFSM parentFSM;
        public EnemyBehaviour enemy;

        public BaseState(AttackFSM parentFSM, EnemyBehaviour enemy)
        {
            this.parentFSM = parentFSM;
            this.enemy = enemy;
        }

        public abstract void OnEnter();

        public abstract void Update();

        public abstract void FixedUpdate();

        public abstract void OnExit();
    }

    public class StaggerState : BaseState
    {
        private readonly float staggerFrames;

        private float currentFrame;

        public StaggerState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            float staggerFrames)
            : base(parentFSM, enemy)
        {
            this.staggerFrames = staggerFrames;

            currentFrame = .0f;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.yellow);
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= staggerFrames)
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
