using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControl : MonoBehaviour
{
    private PlayerConfig config;
    private PlayerBehaviour player;

    public Dictionary<PAttack.Attack, GameObject> attacks = new();

    private void Awake()
    {
        AttackBehaviour.AttackHitEnemy += HitEnemy;
        AttackBehaviour.ParryEnemy += ParryEnemy;

        config = GetComponent<PlayerConfig>();
        player = GetComponent<PlayerBehaviour>();

        SetUpAttackDictionary();
    }

    private void SetUpAttackDictionary()
    {
        foreach (var attack in config.attacks)
        {
            SetUpSpecificAttackBehaviour(attack);

            GameObject hitbox = (GameObject)Instantiate(attack.hitbox, transform);
            hitbox.SetActive(false);

            attacks.Add(attack, hitbox);
        }
    }

    public void HitEnemy(float charge, PAttack.Attack attack, GameObject target)
    {
        float potentialToIncrease = attack.potentialGenerated * charge;

        player.IncreasePotential(potentialToIncrease);

        attack.Hit(attack, target);
    }

    public void ParryEnemy(PAttack.Attack attack)
    {
        player.IncreasePotential(attack.potentialGenerated);
    }

    public void EnableAttackCollider(PAttack.Attack attack)
    {
        attacks[attack].GetComponent<AttackBehaviour>().UpdateDirection(player.lookDirection);

        attacks[attack].SetActive(true);
        attacks[attack].GetComponent<Collider2D>().enabled = true;
    }

    public void DisableAttackCollider(PAttack.Attack attack)
    {
        attacks[attack].SetActive(false);
        attacks[attack].GetComponent<Collider2D>().enabled = false;
    }

    #region Attack Methods
    public void SAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void SSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void SSSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void HSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void FHSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void THSAttackMethod(PAttack.Attack attack, GameObject target)
    {
        var hitEnemyBehaviour = target.GetComponent<EnemyBehaviour>();

        if (hitEnemyBehaviour != null)
        { hitEnemyBehaviour.SetLink(attacks[attack]); }
        else
        { Debug.Log("Attack target invelid, couldn't link."); }
    }
    public void BHSAttackMethod(PAttack.Attack attack, GameObject target)
    {
        var hitEnemyBehaviour = target.GetComponent<EnemyBehaviour>();
        if (hitEnemyBehaviour != null)
        { hitEnemyBehaviour.Knockup(config.BHSKnockupStrength); }
        else
        { Debug.Log("Attack target invalid, couldn't apply knockup."); }
    }
    public void jSAttackMethod(PAttack.Attack attack, GameObject target)
    {
        player.SetSpeedY(5);
    }
    public void jTSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void jBSAttackMethod(PAttack.Attack attack, GameObject target)
    {
        player.SetSpeedY(config.pogoSpeed);
    }
    public void jHSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void jFHSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void jTHSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    public void jBHSAttackMethod(PAttack.Attack attack, GameObject target)
    { }
    private void SetUpSpecificAttackBehaviour(PAttack.Attack attack)
    {
        switch (attack.attackType)
        {
            case PAttack.AttackName.S:
                attack.onHitAttackMethod = SAttackMethod;
                break;

            case PAttack.AttackName.SS:
                attack.onHitAttackMethod = SSAttackMethod;
                break;

            case PAttack.AttackName.SSS:
                attack.onHitAttackMethod = SSSAttackMethod;
                break;

            case PAttack.AttackName.HS:
                attack.onHitAttackMethod = HSAttackMethod;
                break;

            case PAttack.AttackName.FHS:
                attack.onHitAttackMethod = FHSAttackMethod;
                break;

            case PAttack.AttackName.THS:
                attack.onHitAttackMethod = THSAttackMethod;
                break;

            case PAttack.AttackName.BHS:
                attack.onHitAttackMethod = BHSAttackMethod;
                break;

            case PAttack.AttackName.jS:
                attack.onHitAttackMethod = jSAttackMethod;
                break;

            case PAttack.AttackName.jTS:
                attack.onHitAttackMethod = jTSAttackMethod;
                break;

            case PAttack.AttackName.jBS:
                attack.onHitAttackMethod = jBSAttackMethod;
                break;

            case PAttack.AttackName.jHS:
                attack.onHitAttackMethod = jHSAttackMethod;
                break;

            case PAttack.AttackName.jFHS:
                attack.onHitAttackMethod = jFHSAttackMethod;
                break;

            case PAttack.AttackName.jTHS:
                attack.onHitAttackMethod = jTHSAttackMethod;
                break;

            case PAttack.AttackName.jBHS:
                attack.onHitAttackMethod = jBHSAttackMethod;
                break;
        }
    }
    #endregion
}