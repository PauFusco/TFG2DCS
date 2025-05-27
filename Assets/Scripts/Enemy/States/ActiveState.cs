using UnityEngine;

namespace EFSM
{
    public class ActiveState : BaseState
    {
        private readonly Collider2D attackCollider;
        private readonly float activeFrames;

        private float currentFrame;

        public ActiveState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            GameObject attackHitbox,
            float activeFrames) : base(parentFSM, enemy)
        {
            attackCollider = attackHitbox.GetComponent<Collider2D>();
            this.activeFrames = activeFrames;

            attackCollider.enabled = true;

            currentFrame = .0f;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.red);
            currentFrame = .0f;
            attackCollider.enabled = true;
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
            attackCollider.enabled = false;
        }
    }
}
