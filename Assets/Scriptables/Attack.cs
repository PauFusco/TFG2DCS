using System;
using UnityEngine;

namespace PAttack {
    [Serializable]
    [CreateAssetMenu(fileName = "New Attack", menuName = "Scriptable Object/Attack")]
    public class Attack : ScriptableObject
    {
        [Header("Frame Durations")]
        public float anticipation; public float active; public float recovery;

        [Header("HitBox")]
        public UnityEngine.Object hitbox;

        [Header("Attack Input")]
        public CustomInputControl.InputReference[] input;

        [Header("Interactions")]
        public bool chargeable; public bool cancellable; public Attack[] cancellableInto;
    }
}