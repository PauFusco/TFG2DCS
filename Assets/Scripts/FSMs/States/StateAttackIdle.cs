namespace PFSM
{
    public class AIdleState : BaseState
    {
        public AIdleState(
            PlayerFSM parentFSM,
            PlayerBehaviour player)
            : base(parentFSM, player)
        { }

        public override BaseState HandleInput(CustomInputControl.InputState input)
        {
            foreach(PAttack.Attack attack in player.attacks)
            {
                //if(attack)
            }

            return AttackFSM.idle;
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }
}