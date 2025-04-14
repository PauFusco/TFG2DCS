using UnityEditor;
using UnityEngine;

namespace PAttack {
    [CreateAssetMenu(fileName = "New Attack", menuName = "Scriptable Object/Attack")]
    public class Attack : ScriptableObject
    {
        [Header("Frame Durations")]
        public float anticipation; public float strike; public float recovery;

        [Header("HitBox")]
        public Collider2D hitbox;

        [Header("Interactions")]
        public bool cancellable; public Attack[] cancellableInto;
    }
}