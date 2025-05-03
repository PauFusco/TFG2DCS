using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomInputControl
{
    [Serializable]
    public class PlayerInputController : MonoBehaviour
    {
        public InputState input;

        [SerializeField] private bool useController;

        private InputState prevFrameInput;
        private bool controllerConnected;

        private void Awake()
        {
            input = new();
            prevFrameInput = new();

            useController = Gamepad.current != null;
            controllerConnected = Gamepad.current != null;
        }

        // Update is called once per frame
        void Update()
        {
            // Set Controller Mode
            controllerConnected = Gamepad.current != null;

            if (useController && !controllerConnected) useController = false;

            if (Input.GetKeyDown(KeyCode.F5))
            {
                if (useController) useController = false;
                else useController = controllerConnected;
            }

            #region Movement
            input.movement = new(0.0f, 0.0f);
            // Keyboard
            if (!useController)
            {
                if (Input.GetKey(KeyCode.A))
                { input.movement.x = -1.0f; }
                if (Input.GetKey(KeyCode.D))
                { input.movement.x = 1.0f; }
            }
            // Controller
            else
            { input.movement = Gamepad.current.leftStick.value; }
            #endregion
            #region Direction
            // Keyboard
            if (!useController)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    input.left =
                        (prevFrameInput?.left == KeyState.DOWN ||
                        prevFrameInput?.left == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.left = KeyState.UP;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    input.right =
                        (prevFrameInput?.right == KeyState.DOWN ||
                        prevFrameInput?.right == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.right = KeyState.UP;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    input.up =
                        (prevFrameInput?.up == KeyState.DOWN ||
                        prevFrameInput?.up == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.up = KeyState.UP;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    input.down =
                        (prevFrameInput?.down == KeyState.DOWN ||
                        prevFrameInput?.down == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.down = KeyState.UP;
                }
            }
            // Controller
            else
            {
                if (controllerConnected)
                {
                    Vector2 stick = Gamepad.current.leftStick.value.normalized;

                    if (stick.x < 0.0f &&
                         0.71f >= stick.y &&
                        -0.71f < stick.y)
                    {
                        input.left =
                            (prevFrameInput?.left == KeyState.DOWN ||
                            prevFrameInput?.left == KeyState.REPEAT) ?
                            KeyState.REPEAT :
                            KeyState.DOWN;
                    }
                    else
                    {
                        input.left = KeyState.UP;
                    }
                    if (stick.x > 0.0f &&
                         0.71f >= stick.y &&
                        -0.71f < stick.y)
                    {
                        input.right =
                            (prevFrameInput?.right == KeyState.DOWN ||
                            prevFrameInput?.right == KeyState.REPEAT) ?
                            KeyState.REPEAT :
                            KeyState.DOWN;
                    }
                    else
                    {
                        input.right = KeyState.UP;
                    }
                    if (stick.y > 0.0f &&
                         0.71f >= stick.x &&
                        -0.71f < stick.x)
                    {
                        input.up =
                            (prevFrameInput?.up == KeyState.DOWN ||
                            prevFrameInput?.up == KeyState.REPEAT) ?
                            KeyState.REPEAT :
                            KeyState.DOWN;
                    }
                    else
                    {
                        input.up = KeyState.UP;
                    }
                    if (stick.y < 0.0f &&
                         0.71f >= stick.x &&
                        -0.71f < stick.x)
                    {
                        input.down =
                            (prevFrameInput?.down == KeyState.DOWN ||
                            prevFrameInput?.down == KeyState.REPEAT) ?
                            KeyState.REPEAT :
                            KeyState.DOWN;
                    }
                    else
                    {
                        input.down = KeyState.UP;
                    }
                }
            }
            #endregion
            #region Actions
            // Keyboard
            if (!useController)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    input.dash =
                        (prevFrameInput?.dash == KeyState.DOWN ||
                        prevFrameInput?.dash == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.dash = KeyState.UP;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    input.jump =
                        (prevFrameInput?.jump == KeyState.DOWN ||
                        prevFrameInput?.jump == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.jump = KeyState.UP;
                }
            }
            // Controller
            else
            {
                if (Gamepad.current.leftShoulder.ReadValue() != .0f)
                {
                    input.dash =
                        (prevFrameInput?.dash == KeyState.DOWN ||
                        prevFrameInput?.dash == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.dash = KeyState.UP;
                }
                if (Gamepad.current.buttonSouth.ReadValue() != .0f)
                {
                    input.jump =
                        (prevFrameInput?.jump == KeyState.DOWN ||
                        prevFrameInput?.jump == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.jump = KeyState.UP;
                }
            }
            #endregion
            #region Attacks
            // Keyboard
            if (!useController)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    input.S =
                        (prevFrameInput.S == KeyState.DOWN ||
                        prevFrameInput.S == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.S = KeyState.UP;
                }
                if (Input.GetKey(KeyCode.L))
                {
                    input.HS =
                        (prevFrameInput.HS == KeyState.DOWN ||
                        prevFrameInput.HS == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.HS = KeyState.UP;
                }
            }
            else
            {
                if (Gamepad.current.buttonWest.ReadValue() != .0f)
                {
                    input.S =
                        (prevFrameInput.S == KeyState.DOWN ||
                        prevFrameInput.S == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.S = KeyState.UP;
                }
                if (Gamepad.current.buttonNorth.ReadValue() != .0f)
                {
                    input.HS =
                        (prevFrameInput.HS == KeyState.DOWN ||
                        prevFrameInput.HS == KeyState.REPEAT) ?
                        KeyState.REPEAT :
                        KeyState.DOWN;
                }
                else
                {
                    input.HS = KeyState.UP;
                }
            }
            #endregion

            prevFrameInput = input;
        }
    }

    [Serializable]
    public class InputState
    {
        public Vector2 movement;
        public KeyState up, left, down, right;
        public KeyState dash, jump;
        public KeyState S, HS;

        public InputState()
        {
            Reset();
        }

        public void Reset()
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

        public bool Compare(InputReference[] inputRefs, bool currentPlayerAirborneState)
        {
            InputCompareObject currentInput = new(this);

            foreach (var inputRef in inputRefs)
            {
                bool result =
                    currentInput.up == inputRef.up &&
                    currentInput.left == inputRef.left &&
                    currentInput.down == inputRef.down &&
                    currentInput.right == inputRef.right &&
                    currentInput.dash == inputRef.dash &&
                    currentInput.jump == inputRef.jump &&
                    currentInput.slash == inputRef.slash &&
                    currentInput.heavySlash == inputRef.heavySlash &&
                    currentPlayerAirborneState == inputRef.airBorne;

                if (result) 
                    return true;
            }

            return false;
        }

        public bool Compare(InputReference[] inputRefs, KeyState keyState, bool currentPlayerAirborneState)
        {
            foreach (var inputRef in inputRefs)
            {
                InputCompareObject currentInput = new(this, inputRef, keyState);

                bool result =
                    currentInput.up == inputRef.up &&
                    currentInput.left == inputRef.left &&
                    currentInput.down == inputRef.down &&
                    currentInput.right == inputRef.right &&
                    currentInput.dash == inputRef.dash &&
                    currentInput.jump == inputRef.jump &&
                    currentInput.slash == inputRef.slash &&
                    currentInput.heavySlash == inputRef.heavySlash &&
                    currentPlayerAirborneState == inputRef.airBorne;

                if (result)
                    return true;
            }

            return false;
        }
    }

    public class InputCompareObject
    {
        public bool up, left, down, right = false;
        public bool dash, jump = false;
        public bool slash, heavySlash = false;

        public InputCompareObject(InputState inputState)
        {
            up = (inputState.up == KeyState.DOWN || inputState.up == KeyState.REPEAT);
            left = (inputState.left == KeyState.DOWN || inputState.left == KeyState.REPEAT);
            down = (inputState.down == KeyState.DOWN || inputState.down == KeyState.REPEAT);
            right = (inputState.right == KeyState.DOWN || inputState.right == KeyState.REPEAT);
            dash = (inputState.dash == KeyState.DOWN || inputState.dash == KeyState.REPEAT);
            jump = (inputState.jump == KeyState.DOWN || inputState.jump == KeyState.REPEAT);
            slash = (inputState.S == KeyState.DOWN);
            heavySlash = (inputState.HS == KeyState.DOWN);
        }

        public InputCompareObject(InputState inputState, InputReference inputRef, KeyState keyState)
        {
            up = inputRef.up;
            left = inputRef.left;
            down = inputRef.down;
            right = inputRef.right;
            dash = inputRef.dash;
            jump = inputRef.jump;
            slash = (inputState.S == keyState);
            heavySlash = (inputState.HS == keyState);
        }
    }

    public enum KeyState
    {
        DOWN,
        REPEAT,
        UP,
    }
}