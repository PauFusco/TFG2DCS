using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float mSpeed;

    private PFSM.MovementFSM movementFSM;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        movementFSM = new(this);
    }

    private void Update()
    {
        movementFSM.Update();
    }

    private void FixedUpdate()
    {
        movementFSM.FixedUpdate();
    }

    public void Move(Vector2 value)
    {
        Debug.Log(value);
        rigidBody.linearVelocity = new(value.x * mSpeed, value.y);
    }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    {
        movementFSM.HandleInput(this, ctx);
    }
}