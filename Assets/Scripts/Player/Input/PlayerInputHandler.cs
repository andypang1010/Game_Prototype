using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CrouchInput { get; private set; }
    public bool SprintInput { get; private set; }

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
            JumpInput = true;
            jumpInputStartTime = Time.time;
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
