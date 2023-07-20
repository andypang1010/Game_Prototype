using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]
public class Crouch : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float crouchMultiplier = 0.5f;

    private bool isCrouching = false;
    private bool desireCrouch = false;
    private Vector3 crouchSize, idleSize;

    private new Collider2D collider;
    private new Rigidbody2D rigidbody;
    private Controller controller;
    private Ceiling ceiling;
    private Ground ground;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();

        crouchSize = new Vector3(1, crouchMultiplier, 1);
        idleSize = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        isCrouching = controller.input.RetrieveCrouchInput();
    }

    private void FixedUpdate()
    {
        if (isCrouching) SetSize(crouchSize);

        else SetSize(idleSize);
    }

    private void SetSize(Vector3 scale) {
        transform.localScale = scale;
        collider.transform.localScale = scale;
    }
}
