using UnityEngine;

namespace PS
{
    public class PlayerState
    {
        public static IdleState idle;
    }

    public class IdleState : PlayerState
    {
    }

    // General Move state, inherit run and walk from there?
    public class WalkState : PlayerState
    {
    }
}