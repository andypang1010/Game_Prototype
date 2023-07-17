using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract bool RetrieveJumpInput();

    public abstract bool RetrieveCrouchInput();

    public abstract bool RetrieveSprintInput();

    public abstract float RetrieveMoveInput();

    public abstract float RetrieveClimbInput();
}
