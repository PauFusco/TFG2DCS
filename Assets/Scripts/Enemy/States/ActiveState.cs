using UnityEngine;

namespace EFSM
{
    public class ActiveState : BaseState
    {
        private readonly GameObject attackHitbox;
        private readonly float activeFrames;

        private float currentFrame;

        public ActiveState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            GameObject attackHitbox,
            float activeFrames) : base(parentFSM, enemy)
        {
            this.attackHitbox = attackHitbox;
            this.activeFrames = activeFrames;

            currentFrame = .0f;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.red);
            currentFrame = .0f;
            attackHitbox.SetActive(true);
        }

        public override void Update()
        {
            if(currentFrame>= activeFrames)
            {
                parentFSM.ChangeState(AttackFSM.recovery);
            }
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        {
            attackHitbox.SetActive(false);
        }
    }
}
