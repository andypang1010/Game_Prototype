using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]
public class Crouch : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float crouchSize = 0.5f;

    private bool isCrouching = false;
    private bool desireCrouch = false;

    private new Rigidbody2D rigidbody;
    private Controller controller;
    private Ceiling ceiling;
    private Ground ground;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
    }

    private void Update()
    {
        isCrouching = controller.input.RetrieveCrouchInput();
    }

    private void FixedUpdate()
    {
        if (isCrouching)
        {
            transform.localScale = new Vector3(1, crouchSize, 1);
        }

        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
