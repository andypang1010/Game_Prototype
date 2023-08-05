using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public int normalizedInputX { get; private set; }
    public int normalizedInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
        normalizedInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        normalizedInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            jumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            jumpInputStop = true;
        }
    }

    public void UseJumpInput() => jumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
<<<<<<< Updated upstream
        print("Crouch input");
=======
        if (context.started)
        {
            CrouchInput = true;
        }
        else if (context.canceled)
        {
            CrouchInput = false;
        }
>>>>>>> Stashed changes
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
<<<<<<< Updated upstream
        print("Sprint input");
=======
        if (context.started)
        {
            SprintInput = true;
        }
        else if (context.canceled)
        {
            SprintInput = false;
        }
>>>>>>> Stashed changes
    }
}
