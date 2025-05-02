using UnityEngine;

namespace PFSM
{
    public class MIdleState : BaseState
    {
        public MIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        {
            thisMoveState = MoveStateE.IDLE;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            Vector2 noMove = new(.0f, .0f);
            if (input.movement != noMove)
            {
                float speedMult = input.movement.x;

                player.lookDirection = speedMult >= 0;

                MovementFSM.accelerate.SetData(speedMult);
                return MovementFSM.accelerate;
            }

            return MovementFSM.idle;
        }

        public override void OnEnter()
        { }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        {

        }
    }
}