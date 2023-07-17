using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return jumpEnabled && Input.GetButtonDown("Jump");
    }

    public override bool RetrieveCrouchInput()
    {
        return crouchEnabled &&
            (Input.GetKey(KeyCode.LeftCommand) ||
            Input.GetKey(KeyCode.LeftControl));
    }

    public override bool RetrieveSprintInput()
    {
        return sprintEnabled && Input.GetKey(KeyCode.LeftShift);
    }

    public override float RetrieveClimbInput()
    {
        return climbEnabled ? Input.GetAxis("Vertical") : 0;
    }

    public override float RetrieveMoveInput()
    {
        return moveEnabled ? Input.GetAxis("Horizontal") : 0;
    }
}
