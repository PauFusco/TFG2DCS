using UnityEngine.InputSystem;

namespace PFSM
{
    public class FreeFallState : BaseState
    {
        private readonly float baseGravityMultiplier;
        private readonly float fallGravityMultiplier;

        public FreeFallState(
            PlayerFSM parentFSM,
            PlayerBehaviour player,
            float baseGravityMultiplier,
            float fallGravityMultiplier)
            : base(parentFSM, player)
        {
            this.baseGravityMultiplier = baseGravityMultiplier;
            this.fallGravityMultiplier = fallGravityMultiplier;

            thisJumpState = JumpStateE.FREEFALL;
        }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            return JumpFSM.freeFall;
        }

        public override void OnEnter()
        {
            player.rigidBody.gravityScale = fallGravityMultiplier;
        }

        public override void Update()
        {
            if (!player.airborne)
            {
                parentFSM.ChangeState(JumpFSM.ground);
            }

            if(player.rigidBody.gravityScale != 0)
            {
                if (player.invulnerable) player.rigidBody.gravityScale = .0f;
            }
            else
            {
                if (!player.invulnerable) player.rigidBody.gravityScale = fallGravityMultiplier;
            }
        }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        {
            player.rigidBody.gravityScale = baseGravityMultiplier;
        }

    }
}