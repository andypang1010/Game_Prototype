using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public int normalizedInputX { get; private set; }
    public int normalizedInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool crouchInput { get; private set; }
    public bool sprintInput { get; private set; }

    [SerializeField]
    private readonly float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
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

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.started) {
            CrouchInput = true;
        }

        else if (context.canceled) {
            CrouchInput = false;
        }
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.started) {
           SprintInput = true;
        }
        else if (context.canceled) {
            SprintInput = false;
        }
    }
}
