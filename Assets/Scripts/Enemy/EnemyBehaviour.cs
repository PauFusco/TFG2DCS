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
        stunFrames;

    private EnemyConfig config;

    private EFSM.AttackFSM attackFSM;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

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
    }

    private void FixedUpdate()
    {
        attackFSM.FixedUpdate();
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
        charge += amount;

        if (charge >= maxCharge)
        {
            charge = maxCharge;
            attackFSM.ChangeState(EFSM.AttackFSM.stun);
        }
    }

    public void ResetCharge()
    {
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
    }

    public void SetEnemyColor(Color color)
    {
        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy: " + collision.gameObject.layer.ToString());
    }
}
