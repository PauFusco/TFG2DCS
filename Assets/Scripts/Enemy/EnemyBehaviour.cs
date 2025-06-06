using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private Slider chargeSlider;

    private float
        attackCooldown,
        anticipationFrames,
        activeFrames,
        recoveryFrames,
        staggerFrames,
        stunFrames,
        linkSpeed;

    private EnemyConfig config;

    private EFSM.AttackFSM attackFSM;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public bool IsLinked { get; private set; }
    private GameObject linkedGO;

    [SerializeField] private float charge, maxCharge;

    private void Awake()
    {
        AttackBehaviour.HitEnemy += BeAttackedCasual;
        AttackBehaviour.ParryEnemy += BeParriedCasual;
        AttackBehaviour.ExitAttackCollision += ExitAttack;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        config = GetComponent<EnemyConfig>();

        UpdateConfig();

        attackFSM = new(
            this,
            attackHitbox,
            attackCooldown,
            anticipationFrames,
            activeFrames,
            recoveryFrames,
            staggerFrames,
            stunFrames);
    }

    private void Update()
    {
        attackFSM.Update();

        if (IsLinked)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                linkedGO.transform.position, +
                linkSpeed);
        }

        UpdateUI();
    }

    private void FixedUpdate()
    { attackFSM.FixedUpdate(); }

    private void UpdateUI()
    {
        chargeSlider.value = charge / 100;

        if (chargeSlider.value <= 0)
            chargeSlider.gameObject.SetActive(false);
        else
            chargeSlider.gameObject.SetActive(true);
    }

    public void SetLink(GameObject linkedGO)
    {
        if (!IsStunned()) return;

        this.linkedGO = linkedGO;
        IsLinked = true;
    }

    public void SeverLink()
    {
        linkedGO = null;
        IsLinked = false;
    }

    public void Knockup(float strength)
    {
        if (!IsStunned()) return;
        rigidBody.AddForceY(strength, ForceMode2D.Impulse);
    }

    public void BeAttackedCasual(float charge, PAttack.Attack attack)
    {
        AddCharge(charge * attack.maxChargeInflicted);
    }

    public void ExitAttack(PAttack.Attack attack)
    {
        if (IsLinked) { SeverLink(); }

        if (IsAirborne() &&
            attack.attackType == PAttack.AttackTypes.THS)
        {
            SetSpeedY(0);
        }
    }

    public void BeParriedCasual(PAttack.Attack attack)
    {
        AddCharge(attack.maxChargeInflicted);

        if (!IsStunned())
        {
            attackFSM.ChangeState(EFSM.AttackFSM.stagger);
        }
    }

    private void AddCharge(float amount)
    {
        if (IsStunned()) return;

        charge += amount;

        if (charge >= maxCharge)
        {
            charge = maxCharge;
            attackFSM.ChangeState(EFSM.AttackFSM.stun);
        }
    }

    public void ResetCharge()
    {
        if (IsLinked) SeverLink();

        charge = 0;
    }

    void UpdateConfig()
    {
        attackCooldown = config.attackCooldown;
        anticipationFrames = config.anticipationFrames;
        activeFrames = config.activeFrames;
        recoveryFrames = config.recoveryFrames;
        staggerFrames = config.staggerFrames;
        stunFrames = config.stunFrames;
        linkSpeed = config.linkSpeed;

        rigidBody.gravityScale = config.baseGravity;
    }

    public void SetEnemyColor(Color color)
    { spriteRenderer.color = color; }

    public bool IsStunned()
    { return attackFSM.currentState == EFSM.AttackFSM.stun; }

    public bool IsAirborne()
    { return rigidBody.linearVelocity.y != 0; }

    public void SetSpeedY(float value)
    { rigidBody.linearVelocityY = value; }
}
