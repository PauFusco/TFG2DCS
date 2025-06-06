using System;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private PAttack.Attack attack;

    private float oPositionX;

    public static event Action<float, PAttack.Attack, EnemyBehaviour> AttackHitEnemy;
    public static event Action<float, PAttack.Attack> HitEnemy;
    public static event Action<PAttack.Attack> ParryEnemy;

    public static event Action<PAttack.Attack> ExitAttackCollision;

    private void Awake()
    {
        oPositionX = transform.localPosition.x;
    }
    public void UpdateDirection(bool currentPlayerDirection)
    {
        var tempPos = transform.localPosition;
        tempPos.x = oPositionX * (currentPlayerDirection ? 1 : -1);

        transform.localPosition = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && attack.attackType != PAttack.AttackTypes.Parry)
        {
            var hitEnemyBehaviour = collision.gameObject.GetComponent<EnemyBehaviour>();
            if (hitEnemyBehaviour != null)
            {
                AttackHitEnemy?.Invoke(PFSM.AttackFSM.active.GetAttackCharge(), attack, hitEnemyBehaviour);
                HitEnemy?.Invoke(PFSM.AttackFSM.active.GetAttackCharge(), attack);
            }
        }

        if (collision.gameObject.layer == 8 && attack.attackType == PAttack.AttackTypes.Parry)
        {
            ParryEnemy?.Invoke(attack);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            ExitAttackCollision?.Invoke(attack);
        }
    }
}