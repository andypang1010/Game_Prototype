using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private bool isIdle, isMoving, isCrouching, isSprinting, isJumping, isClimbing;
    private bool canCrouch, canSprint, canJump, canClimb;

    private Controller controller;
    private InputController input;

    void Start() {
        controller = GetComponent<Controller>();
        input = controller.input;
    }

    void Update()
    {
        if (input.RetrieveMoveInput() != 0) {

        }
    }
}
