using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private InputState input;
    [SerializeField] private bool useController;

    private InputState prevFrameInput;
    private bool controllerConnected;

    private void Awake()
    {
        input = new();
        useController = false;
        controllerConnected = Gamepad.current != null;
    }

    // Update is called once per frame
    void Update()
    {
        // Set Controller Mode
        if (Input.GetKeyDown(KeyCode.F5))
        { useController = !useController; }

        input.Reset();

        controllerConnected = Gamepad.current != null;

        #region Movement
        if (Input.GetKey(KeyCode.A))
        { input.movement.x -= 1.0f; }
        if (Input.GetKey(KeyCode.D))
        { input.movement.x += 1.0f; }
        if (controllerConnected)
        { input.movement = Gamepad.current.leftStick.value; }
        #endregion
        #region Direction
        if (Input.GetKey(KeyCode.A))
        {
            input.left =
                (prevFrameInput?.left == InputState.KeyState.DOWN ||
                prevFrameInput?.left == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.left = InputState.KeyState.UP;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.right =
                (prevFrameInput?.right == InputState.KeyState.DOWN ||
                prevFrameInput?.right == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.right = InputState.KeyState.UP;
        }
        if (Input.GetKey(KeyCode.W))
        {
            input.up =
                (prevFrameInput?.up == InputState.KeyState.DOWN ||
                prevFrameInput?.up == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.up = InputState.KeyState.UP;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.down =
                (prevFrameInput?.down == InputState.KeyState.DOWN ||
                prevFrameInput?.down == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.down = InputState.KeyState.UP;
        }
        #endregion
        #region Actions
        if (Input.GetKey(KeyCode.LeftShift))
        {
            input.dash =
                (prevFrameInput?.dash == InputState.KeyState.DOWN ||
                prevFrameInput?.dash == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.dash = InputState.KeyState.UP;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            input.jump =
                (prevFrameInput?.jump == InputState.KeyState.DOWN ||
                prevFrameInput?.jump == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.jump = InputState.KeyState.UP;
        }
        #endregion
        #region Attacks
        if (Input.GetKey(KeyCode.K))
        {
            input.S =
                (prevFrameInput?.S == InputState.KeyState.DOWN ||
                prevFrameInput?.S == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.S = InputState.KeyState.UP;
        }
        if (Input.GetKey(KeyCode.L))
        {
            input.HS =
                (prevFrameInput?.HS == InputState.KeyState.DOWN ||
                prevFrameInput?.HS == InputState.KeyState.REPEAT) ?
                InputState.KeyState.REPEAT :
                InputState.KeyState.DOWN;
        }
        else
        {
            input.HS = InputState.KeyState.UP;
        }
        #endregion

        prevFrameInput = input;
    }
}

[Serializable]
public class InputState
{
    public enum KeyState
    {
        DOWN,
        REPEAT,
        UP,
    }

    public Vector2 movement;
    public KeyState up, left, down, right;
    public KeyState dash, jump;
    public KeyState S, HS;

    public InputState()
    {
        movement = new(.0f, .0f);

        up = KeyState.UP;
        left = KeyState.UP;
        down = KeyState.UP;
        right = KeyState.UP;

        dash = KeyState.UP;
        jump = KeyState.UP;

        S = KeyState.UP;
        HS = KeyState.UP;
    }

    public void Reset()
    {
        movement = new(.0f, .0f);

        dash = KeyState.UP;
        jump = KeyState.UP;

        S = KeyState.UP;
        HS = KeyState.UP;
    }
}