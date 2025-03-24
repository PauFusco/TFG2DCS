using UnityEngine;

namespace PS
{
    public class PlayerState
    {
        public static IdleState idle;
        public static WalkState walk;

        public PlayerState HandleInput()
        {
            // Check for other inputs and return corresponding state

            return null;
        }
    }

    public class IdleState : PlayerState
    {
    }

    // General Move state, inherit run and walk from there?
    public class WalkState : PlayerState
    {
    }
}