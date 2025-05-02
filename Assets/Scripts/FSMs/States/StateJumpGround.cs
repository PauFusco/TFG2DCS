using UnityEngine.InputSystem;
using UnityEngine;

namespace PFSM
{
    public class GroundState : BaseState
    {
        public GroundState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        {
            thisJumpState = JumpStateE.GROUND;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            if(input.jump == CustomInputControl.KeyState.DOWN &&
                player.grounded &&
                player.GetFSM(player.dashFSMIdx).currentState == DashFSM.idle)
            {
                player.grounded = false;
                return JumpFSM.jump;
            }

            return JumpFSM.ground;
        }

        public override void OnEnter()
        { }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}