using UnityEngine;

public class EnemyConfig : MonoBehaviour
{
    [Header("Attack")]
    public float attackCooldown; public float anticipationFrames; public float activeFrames;
    public float recoveryFrames; public float staggerFrames; public float stunFrames;
}
