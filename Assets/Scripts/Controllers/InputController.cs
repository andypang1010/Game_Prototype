using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    [SerializeField]
    public bool moveEnabled { protected set; get; } = true;

    [SerializeField]
    public bool sprintEnabled { protected set; get; } = true;

    [SerializeField]
    public bool crouchEnabled { protected set; get; } = true;

    [SerializeField]
    public bool jumpEnabled { protected set; get; } = true;

    [SerializeField]
    public bool climbEnabled { protected set; get; } = true;

    public abstract bool RetrieveJumpInput();

    public abstract bool RetrieveCrouchInput();

    public abstract bool RetrieveSprintInput();

    public abstract float RetrieveMoveInput();

    public abstract float RetrieveClimbInput();
}
