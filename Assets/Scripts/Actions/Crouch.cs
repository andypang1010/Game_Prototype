using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]
public class Crouch : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float crouchMultiplier = 0.65f;
    private bool isCrouching = false;
    private bool desireCrouch = false;
    private float moveMaxSpeed, moveMaxAcceleration, crouchMaxSpeed, crouchMaxAcceleration;

    private new Rigidbody2D rigidbody;
    private Controller controller;
    private Ceiling ceiling;
    private Ground ground;
    private Move move;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
        move = GetComponent<Move>();

        moveMaxSpeed = move.GetMaxSpeed();
        moveMaxAcceleration = move.GetMaxAcceleration();

        crouchMaxSpeed = move.GetMaxSpeed() * crouchMultiplier;
        crouchMaxAcceleration = move.GetMaxAcceleration() * crouchMultiplier;
    }

    private void Update()
    {
        isCrouching = controller.input.RetrieveCrouchInput();
    }

    private void FixedUpdate() {
        if (isCrouching) {
            print(crouchMaxSpeed);
            move.SetMaxSpeed(crouchMaxSpeed);
            move.SetMaxAcceleration(crouchMaxAcceleration);
            transform.localScale = new Vector3(1, crouchMultiplier, 1);
        }

        else {
            print(moveMaxSpeed);
            move.SetMaxSpeed(moveMaxSpeed);
            move.SetMaxAcceleration(moveMaxAcceleration);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
