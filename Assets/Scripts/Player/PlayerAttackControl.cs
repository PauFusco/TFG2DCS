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
            attacks.Add(attack, hitbox);

            hitbox.SetActive(false);
            hitbox.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void HitEnemy(float charge, PAttack.Attack attack, EnemyBehaviour enemy)
    {
        float potentialToIncrease = attack.potentialGenerated * charge;

        player.IncreasePotential(potentialToIncrease);

        attack.Hit(attack, enemy);
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
    public void SExecuteAttackMethod()
    { }
    public void SHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void SSExecuteAttackMethod()
    { }
    public void SSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void SSSExecuteAttackMethod()
    { }
    public void SSSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void HSExecuteAttackMethod()
    { }
    public void HSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void FHSExecuteAttackMethod()
    { }
    public void FHSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void THSExecuteAttackMethod()
    {
        player.SetSpeedY(config.THSAirSpeed);
    }
    public void THSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    {
        target.SetLink(attacks[attack]);
    }

    public void BHSExecuteAttackMethod()
    { }
    public void BHSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    {
        target.Knockup(config.BHSKnockupStrength);
    }

    public void jSExecuteAttackMethod()
    { }
    public void jSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    {
        player.SetSpeedY(7);

        if (target.IsAirborne()) target.SetSpeedY(5);
    }

    public void jTSExecuteAttackMethod()
    { }
    public void jTSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void jBSExecuteAttackMethod()
    { }
    public void jBSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    {
        player.GetFSM(PlayerFSMControl.jumpFSMIdx).ChangeState(PFSM.JumpFSM.jump);
    }

    public void jHSExecuteAttackMethod()
    { }
    public void jHSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void jFHSExecuteAttackMethod()
    {
        player.SetSpeedX(config.jFHSSideSpeed * (player.lookDirection ? 1 : -1));
        player.SetSpeedY(0);
    }
    public void jFHSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    {
        target.SetLink(attacks[attack]);
    }

    public void jTHSExecuteAttackMethod()
    { }
    public void jTHSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    { }

    public void jBHSExecuteAttackMethod()
    {
        player.SetSpeedY(config.jBHSDownSpeed);
        player.SetSpeedX(config.jBHSSideSpeed * (player.lookDirection ? 1 : -1));
    }
    public void jBHSHitAttackMethod(PAttack.Attack attack, EnemyBehaviour target)
    {
        target.SetLink(attacks[attack]);
    }

    private void SetUpSpecificAttackBehaviour(PAttack.Attack attack)
    {
        switch (attack.attackType)
        {
            case PAttack.AttackTypes.S:
                attack.onExecuteAttackMethod = SExecuteAttackMethod;
                attack.onHitAttackMethod = SHitAttackMethod;
                break;

            case PAttack.AttackTypes.SS:
                attack.onExecuteAttackMethod = SSExecuteAttackMethod;
                attack.onHitAttackMethod = SSHitAttackMethod;
                break;

            case PAttack.AttackTypes.SSS:
                attack.onExecuteAttackMethod = SSSExecuteAttackMethod;
                attack.onHitAttackMethod = SSSHitAttackMethod;
                break;

            case PAttack.AttackTypes.HS:
                attack.onExecuteAttackMethod = HSExecuteAttackMethod;
                attack.onHitAttackMethod = HSHitAttackMethod;
                break;

            case PAttack.AttackTypes.FHS:
                attack.onExecuteAttackMethod = FHSExecuteAttackMethod;
                attack.onHitAttackMethod = FHSHitAttackMethod;
                break;

            case PAttack.AttackTypes.THS:
                attack.onExecuteAttackMethod = THSExecuteAttackMethod;
                attack.onHitAttackMethod = THSHitAttackMethod;
                break;

            case PAttack.AttackTypes.BHS:
                attack.onExecuteAttackMethod = BHSExecuteAttackMethod;
                attack.onHitAttackMethod = BHSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jS:
                attack.onExecuteAttackMethod = jSExecuteAttackMethod;
                attack.onHitAttackMethod = jSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jTS:
                attack.onExecuteAttackMethod = jTSExecuteAttackMethod;
                attack.onHitAttackMethod = jTSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jBS:
                attack.onExecuteAttackMethod = jBSExecuteAttackMethod;
                attack.onHitAttackMethod = jBSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jHS:
                attack.onExecuteAttackMethod = jHSExecuteAttackMethod;
                attack.onHitAttackMethod = jHSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jFHS:
                attack.onExecuteAttackMethod = jFHSExecuteAttackMethod;
                attack.onHitAttackMethod = jFHSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jTHS:
                attack.onExecuteAttackMethod = jTHSExecuteAttackMethod;
                attack.onHitAttackMethod = jTHSHitAttackMethod;
                break;

            case PAttack.AttackTypes.jBHS:
                attack.onExecuteAttackMethod = jBHSExecuteAttackMethod;
                attack.onHitAttackMethod = jBHSHitAttackMethod;
                break;
        }
    }
    #endregion
}