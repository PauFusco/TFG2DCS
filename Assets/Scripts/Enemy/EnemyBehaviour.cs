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
            staggerFrames);
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
        attackFSM.ChangeState(EFSM.AttackFSM.stagger);
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
