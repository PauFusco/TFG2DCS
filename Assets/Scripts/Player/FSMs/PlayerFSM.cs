namespace PFSM
{
    public enum MoveStateE
    {
        IDLE, ACCELERATE, WALK, DECELERATE, TURN, Default
    }

    public enum JumpStateE
    {
        GROUND, JUMP, FREEFALL, Default
    }

    public abstract class PlayerFSM
    {
        public BaseState currentState;

        protected PlayerBehaviour player;

        public virtual void HandleInput(CustomInputControl.InputState input)
        {
            BaseState checkState = currentState.HandleInput(input);

            if (currentState != checkState) ChangeState(checkState);
        }

        public virtual void Update()
        {
            currentState.Update();
        }

        public virtual void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public virtual void ChangeState(BaseState state)
        {
            currentState.OnExit();
            
            currentState = state;
            
            currentState.OnEnter();
        }
    }
}