using UnityEngine;

namespace EFSM
{
    public class AttackFSM
    {
        public BaseState currentState;

        public static IdleState idle;
        public static AnticipationState anticipation;
        public static ActiveState active;
        public static RecoveryState recovery;
        public static StaggerState stagger;

        protected EnemyBehaviour enemy;
        protected GameObject attackHitbox;

        public AttackFSM(
            EnemyBehaviour enemy,
            GameObject attackHitbox,
            float attackCooldown,
            float anticipationFrames,
            float activeFrames,
            float recoveryFrames,
            float staggerFrames)
        {
            this.enemy = enemy;
            this.attackHitbox = attackHitbox;

            idle = new(this, enemy, attackCooldown);
            anticipation = new(this, enemy, anticipationFrames);
            active = new(this, enemy, attackHitbox, activeFrames);
            recovery = new(this, enemy, recoveryFrames);
            stagger = new(this, enemy, staggerFrames);

            currentState = idle;
        }

        public void Update()
        { currentState.Update(); }

        public void FixedUpdate()
        { currentState.FixedUpdate(); }

        public void ChangeState(BaseState state)
        {
            currentState.OnExit();

            currentState = state;

            currentState.OnEnter();
        }
    }
}
