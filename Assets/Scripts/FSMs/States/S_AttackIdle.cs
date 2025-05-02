using UnityEngine;

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
            foreach (var attack in AttackFSM.attacks)
            {
                if (input.Compare(attack.input))
                {
                    if (attack.chargeable)
                    {
                        AttackFSM.charge.SetData(attack);
                        return AttackFSM.charge;
                    }
                    else
                    {
                        AttackFSM.anticipation.SetData(attack);
                        return AttackFSM.anticipation;
                    }
                }
            }

            return AttackFSM.idle;
        }

        public override void OnEnter()
        {
            player.SetPlayerColor(Color.white);
        }

        public override void Update()
        { }

        public override void FixedUpdate()
        { }

        public override void OnExit()
        { }
    }
}