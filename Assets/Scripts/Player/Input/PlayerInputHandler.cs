using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public int normalizedInputX { get; private set; }
    public int normalizedInputY { get; private set; }
    public bool jumpInput { get; private set; }

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
        if(context.started)
        {
            jumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }

    public void UseJumpInput() => jumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        print("Crouch input");
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        print("Sprint input");
    }
}
