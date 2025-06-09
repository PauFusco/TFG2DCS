using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private Slider potentialSlider;

    [SerializeField] private float currentPotential;
    [SerializeField] private float maxPotential;

    private PlayerFSMControl FSMControl;
    private PlayerAttackControl attackControl;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public bool airborne;
    public bool invulnerable;
    public bool lookDirection;
    public float currentSpeed;

    private void Awake()
    {
        AttackHitbox.HitPlayer += GetHit;

        FSMControl = GetComponent<PlayerFSMControl>();
        attackControl = GetComponent<PlayerAttackControl>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

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

        if (potentialSlider != null)
            UpdateUI();
    }

    private void UpdateUI()
    {
        potentialSlider.value = currentPotential / 100;

        if (potentialSlider.value <= 0)
            potentialSlider.gameObject.SetActive(false);
        else
            potentialSlider.gameObject.SetActive(true);
    }

    public void GetHit()
    {
        Debug.Log("Enemy Hit Player");
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

    public void EnableCurrentAttackCollider(PAttack.Attack currentAttack)
    { attackControl.EnableAttackCollider(currentAttack); }
    public void DisableCurrentAttackCollider(PAttack.Attack currentAttack)
    { attackControl.DisableAttackCollider(currentAttack); }

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