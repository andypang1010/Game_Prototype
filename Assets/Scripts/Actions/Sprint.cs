using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    [SerializeField, Range(1, 2f)] private float sprintMultiplier = 1.8f;
    private bool isSprinting = false;
    private float moveMaxSpeed, moveMaxAcceleration, sprintMaxSpeed, sprintMaxAcceleration;

    private new Rigidbody2D rigidbody;
    private Controller controller;
    private Move move;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
        move = GetComponent<Move>();

        moveMaxSpeed = move.GetMaxSpeed();
        moveMaxAcceleration = move.GetMaxAcceleration();

        sprintMaxSpeed = move.GetMaxSpeed() * sprintMultiplier;
        sprintMaxAcceleration = move.GetMaxAcceleration() * sprintMultiplier;
    }

    private void Update()
    {
        isSprinting = controller.input.RetrieveSprintInput();
    }

    private void FixedUpdate() {
        // If is sprinting, multiply maxSpeed and maxAccelration (in Move) with sprintMultiplier
        if (isSprinting) {
            move.SetMaxSpeed(sprintMaxSpeed);
            move.SetMaxAcceleration(sprintMaxAcceleration);
        }

        // Else, divide sprintMultiplier from maxSpeed and maxAccelration
        else {
            move.SetMaxSpeed(moveMaxSpeed);
            move.SetMaxAcceleration(moveMaxAcceleration);

        }
    }
}
