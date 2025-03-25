using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private PFSM.MovementFSM movementFSM;

    private void Awake()
    {
        movementFSM = new();
    }

    public void Move()
    {
        Debug.Log("Move");
    }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    {
        movementFSM.HandleInput(this, ctx);
    }
}