using UnityEngine;
using UnityEngine.InputSystem;

namespace PAttack {
    [CreateAssetMenu(fileName = "New Attack", menuName = "Scriptable Object/Attack")]
    public class Attack : ScriptableObject
    {
        [Header("Frame Durations")]
        public float anticipation; public float strike; public float recovery;

        [Header("HitBox")]
        public Object hitbox;

        [Header("Attack Input")]
        public InputActionReference input;

        [Header("Interactions")]
        public bool cancellable; public Attack[] cancellableInto;
    }
}