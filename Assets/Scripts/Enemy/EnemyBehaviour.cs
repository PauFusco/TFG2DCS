using System.Runtime.CompilerServices;
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

    [SerializeField] private uint charge, maxCharge;

    private void Awake()
    {
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

    public void BeParriedCasual()
    {
        IncreaseCharge(25);
        
    }

    public void IncreaseCharge(uint amount)
    {
        charge += amount;
        if (charge < maxCharge)
        {
            attackFSM.ChangeState(EFSM.AttackFSM.stagger);
        }
        else
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
}
