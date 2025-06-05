using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject attackHitbox;

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
                linkedGO.transform.position,
                linkSpeed);

            Debug.Log("LINKED");
        }
    }

    private void FixedUpdate()
    {
        attackFSM.FixedUpdate();
    }

    public void SetLink(GameObject linkedGO)
    {
        if (attackFSM.currentState != EFSM.AttackFSM.stun) return;

        this.linkedGO = linkedGO;
        IsLinked = true;
    }

    public void SeverLink()
    {
        linkedGO = null;
        IsLinked = false;
    }

    public void BeAttackedCasual(float charge, PAttack.Attack attack)
    {
        AddCharge(charge * attack.maxChargeInflicted);
    }

    public void BeParriedCasual(PAttack.Attack attack)
    {
        AddCharge(attack.maxChargeInflicted);

        if (attackFSM.currentState != EFSM.AttackFSM.stun)
        {
            attackFSM.ChangeState(EFSM.AttackFSM.stagger);
        }
    }

    private void AddCharge(float amount)
    {
        if (attackFSM.currentState == EFSM.AttackFSM.stun) return;

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
    }

    public void SetEnemyColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
