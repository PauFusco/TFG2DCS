using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject floor;

    [SerializeField] private float currentPotential;
    [SerializeField] private float maxPotential;

    private PlayerConfig config;
    private PlayerFSMControl FSMControl;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public bool airborne;
    public bool invulnerable;
    public bool lookDirection;
    public float currentSpeed;

    public GameObject[] attackHitboxObjs;

    private void Awake()
    {
        AttackBehaviour.HitEnemy += HitEnemy;
        AttackBehaviour.ParryEnemy += ParryEnemy;

        FSMControl = GetComponent<PlayerFSMControl>();
        config = GetComponent<PlayerConfig>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        SpawnAttackHitboxObjs();

        #region Debug Variables
        currentSpeed = .0f;

        airborne = false;
        invulnerable = false;
        lookDirection = true;
        #endregion
    }

    private void Update()
    {
        currentSpeed = rigidBody.linearVelocityX;
    }

    public void HitEnemy(float charge, PAttack.Attack attack)
    {
        float potentialToIncrease = attack.potentialGenerated * charge;

        IncreasePotential(potentialToIncrease);
    }

    public void ParryEnemy(PAttack.Attack attack)
    {
        IncreasePotential(attack.potentialGenerated);
    }

    public void IncreasePotential(float amount)
    {
        currentPotential += amount;

        currentPotential = currentPotential > maxPotential ?
            maxPotential : currentPotential;
    }

    public void ExpendPotential(float amount)
    {
        currentPotential -= amount;
        currentPotential = currentPotential < 0 ?
            0 : currentPotential;
    }

    public bool EnoughPotentialForAttack(float potentialNeeded)
    {
        return potentialNeeded <= currentPotential;
    }

    private void SpawnAttackHitboxObjs()
    {
        attackHitboxObjs = new GameObject[config.attacks.Length];

        for (int i = 0; i < config.attacks.Length; i++)
        {
            attackHitboxObjs[i] = (GameObject)Instantiate(config.attacks[i].hitbox, transform);

            attackHitboxObjs[i].SetActive(false);
        }
    }

    public void EnableCurrentAttackCollider(int currentAttackIdx)
    {
        attackHitboxObjs[currentAttackIdx].GetComponent<AttackBehaviour>().UpdateDirection(lookDirection);

        attackHitboxObjs[currentAttackIdx].SetActive(true);
        attackHitboxObjs[currentAttackIdx].GetComponent<Collider2D>().enabled = true;
    }

    public void DisableCurrentAttackCollider(int currentAttackIdx)
    {
        attackHitboxObjs[currentAttackIdx].SetActive(false);
        attackHitboxObjs[currentAttackIdx].GetComponent<Collider2D>().enabled = false;
    }

    public void SetPlayerColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetSpeedX(float value)
    { rigidBody.linearVelocityX = value; }

    public void SetSpeedY(float value)
    { rigidBody.linearVelocityY = value; }

    public Rigidbody2D GetRigidbody()
    { return rigidBody; }

    public void Dash(float speed)
    { SetSpeedX(speed); }

    public PFSM.PlayerFSM GetFSM(uint index)
    { return FSMControl.GetFSM(index); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(floor))
            airborne = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(floor))
            airborne = true;
    }
}