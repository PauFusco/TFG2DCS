using System;
using UnityEngine;

namespace PAttack
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Attack", menuName = "Scriptable Object/Attack")]
    public class Attack : ScriptableObject
    {
        [Tooltip("The name of the attack")]
        public AttackTypes attackType;

        [Header("Frame Durations")]
        [Tooltip("Number of frames in the attack's anticipation phase.")]
        public float anticipation;
        [Tooltip("Number of frames in the attack's active phase.")]
        public float active;
        [Tooltip("Number of frames in the attack's recovery phase.")]
        public float recovery;

        [Tooltip("Amount of Charge damage the attack will deal when charged to the maximum.")]
        public float maxChargeInflicted;
        [Tooltip("Amount of Potential used when casted.")]
        public float potentialUse;
        [Tooltip("Amount of Potential generated when hitting an enemy.")]
        public float potentialGenerated;

        [Header("HitBox")]
        [Tooltip("A prefab with the hitbox that spawns in the attack's active phase.")]
        public UnityEngine.Object hitbox;

        [Header("Attack Input")]
        [Tooltip("An array with the Input Reference Objects with the possible inputs for the attack.")]
        public CustomInputControl.InputReference[] input;

        [Header("Interactions")]
        [Tooltip("True if the attack will be executed while airborne.")]
        public bool airborne;
        [Tooltip("True if the attack can be charged by holding its button.")]
        public bool chargeable;
        [Tooltip("True if the attack can be cancelled into other attacks.")]
        public bool cancellable;
        [Tooltip("List of attacks it can be cancelled into.")]
        public Attack[] cancellableInto;

        public delegate void AttackEnemyDelegate(Attack attack, 
                                                 EnemyBehaviour target);
        public AttackEnemyDelegate onHitAttackMethod;

        public delegate void AttackDelegate();
        public AttackDelegate onExecuteAttackMethod;

        public void Execute()
        { onExecuteAttackMethod(); }

        public void Hit(Attack attack, EnemyBehaviour target)
        { onHitAttackMethod(attack, target); }
    }

    public enum AttackTypes
    {
        S,      // 5S
        SS,     // 5SS
        SSS,    // 5SSS
        HS,     // 5HS
        FHS,    // 6HS
        THS,    // 8HS
        BHS,    // 2HS
        jS,     // j5S
        jTS,    // j8S
        jBS,    // j2S
        jHS,    // j5HS
        jFHS,   // j6HS
        jTHS,   // j8HS
        jBHS,   // j2HS
        Parry,
    }
}