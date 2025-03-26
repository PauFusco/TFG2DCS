using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float mSpeed;

    private PSFM.PlayerFSM[2] playerFSMs;

    private PFSM.MovementFSM movementFSM;
    private PSFM.JumpFSM jumpFSM;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        movementFSM = new(this);
        jumpFSM = new(this);
        
        playerFSMs = new({movementFSM, jumpFSM});

        grounded = true;
    }

    private void Update()
    {
        foreach (var FSM in playerFSMs)
            FSM.Update();

    }

    private void FixedUpdate()
    {
        foreach (var FSM in playerFSMs)
            FSM.FixedUpdate();
        
    }

    public void Move(Vector2 value)
    {
        rigidBody.linearVelocity = new(value.x * mSpeed, value.y);
    }

    public void Jump()
    {
        Debug.Log("Jump");
    }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    {
        movementFSM.HandleInput(this, ctx);
    }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        jumpFSM.HandleInput(this, ctx);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(contact);
            // If with contact with floor collider call event of input from jumpFSM
        }
        
    }
}