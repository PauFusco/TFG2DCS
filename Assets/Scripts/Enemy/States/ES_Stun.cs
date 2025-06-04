using UnityEngine;
using UnityEngine.UI;

namespace EFSM
{
    public class StunState : BaseState
    {
        private readonly float stunFrames;

        private float currentFrame;

        public StunState(
            AttackFSM parentFSM,
            EnemyBehaviour enemy,
            float stunFrames)
            : base(parentFSM, enemy)
        {
            this.stunFrames = stunFrames;

            currentFrame = .0f;
        }

        public override void OnEnter()
        {
            enemy.SetEnemyColor(Color.magenta);
            currentFrame = .0f;
        }

        public override void Update()
        {
            if(currentFrame > stunFrames)
            {
                parentFSM.ChangeState(AttackFSM.idle);
            }
        }

        public override void FixedUpdate()
        {
            currentFrame++;
        }

        public override void OnExit()
        {
            enemy.ResetCharge();
        }
    }
}
