using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerTest : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        rigidBody2D.linearVelocity = ctx.ReadValue<Vector2>()*10;
    }
}
