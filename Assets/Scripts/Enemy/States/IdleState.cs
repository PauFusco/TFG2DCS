using UnityEngine;

namespace EFSM
{
    public class IdleState : BaseState
    {
        private readonly float attackCooldown;

        private float currentFrame;

        public IdleState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            float attackCooldown)
            : base(parentFSM, enemy)
        {
            this.attackCooldown = attackCooldown;

            currentFrame = .0f;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.white);
            currentFrame = .0f;
        }

        public override void Update()
        {
            if (currentFrame >= attackCooldown)
            {
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
