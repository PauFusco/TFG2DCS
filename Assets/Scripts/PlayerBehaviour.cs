using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float jFallGravityMultiplier;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private PFSM.MovementFSM movementFSM;
    private PFSM.JumpFSM jumpFSM;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        movementFSM = new(this);
        jumpFSM = new(this);
    }

    private void Update()
    {
        movementFSM.Update();
        jumpFSM.Update();
    }

    private void FixedUpdate()
    {
        movementFSM.FixedUpdate();
        jumpFSM.FixedUpdate();
    }

    public void Move(Vector2 value)
    {
        rigidBody.linearVelocityX = value.x * speed;
    }

    public void Jump()
    {
        rigidBody.linearVelocityY = jumpForce;
    }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    {
        movementFSM.HandleInput(this, ctx);
    }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        jumpFSM.HandleInput(this, ctx);
    }
}