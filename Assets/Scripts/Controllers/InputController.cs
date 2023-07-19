using UnityEngine;

public abstract class InputController : ScriptableObject
{
    [SerializeField]
    public bool moveEnabled { protected get; set; } = true;

    [SerializeField]
    public bool sprintEnabled { protected get; set; } = true;

    [SerializeField]
    public bool crouchEnabled { protected get; set; } = true;

    [SerializeField]
    public bool jumpEnabled { protected get; set; } = true;

    [SerializeField]
    public bool climbEnabled { protected get; set; } = true;

    public abstract bool RetrieveJumpInput();

    public abstract bool RetrieveCrouchInput();

    public abstract bool RetrieveSprintInput();

    public abstract float RetrieveMoveInput();

    public abstract float RetrieveClimbInput();
}
