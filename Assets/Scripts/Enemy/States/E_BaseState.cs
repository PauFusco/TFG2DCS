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
}
