using UnityEngine;

namespace PFSM
{
    public class WalkState : BaseState
    {
        private readonly float maxSpeed;

        public float speed;

        public WalkState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float maxSpeed)
            : base(parentFSM, player)
        {
            this.maxSpeed = maxSpeed;
            thisMoveState = MoveStateE.WALK;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            Vector2 noMove = new(.0f, .0f);
            if (input.movement != noMove)
            {
                if (player.lookDirection != input.movement.x >= 0)
                {
                    player.lookDirection = input.movement.x >= 0;
                    MovementFSM.accelerate.SetData(input.movement.x);
                    return MovementFSM.accelerate;
                }

                speed = input.movement.x * maxSpeed;
            }
            else
            {
                return MovementFSM.decelerate;
            }

            return MovementFSM.walk;
        }

        public void SetData(float speed)
        {
            this.speed = speed;
        }

        public override void OnEnter()
        { }

        public override void Update()
        {
            if (player.GetFSM(PlayerFSMControl.dashFSMIdx).currentState == DashFSM.idle &&
                player.GetFSM(PlayerFSMControl.attaFSMIdx).currentState == AttackFSM.idle)
                player.SetSpeedX(speed);
            else
                parentFSM.ChangeState(MovementFSM.decelerate);
        }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}