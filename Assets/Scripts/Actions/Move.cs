using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float walkingMaxSpeed = 5f;
    [SerializeField, Range(0, 100f)] private float walkingMaxAcceleration = 35f;
    [SerializeField, Range(0, 100f)] private float maxAirAcceleration = 20f;
    [SerializeField, Range(1, 2f)] private float sprintMultiplier = 1.8f;
    [SerializeField, Range(0, 1f)] private float crouchMultiplier = 0.65f;

    private Vector2 direction, desiredVelocity, currentVelocity;
    private float maxSpeed, maxAcceleration, maxSpeedChange, acceleration;
    private bool onGround;

    private Ground ground;
    private Controller controller;
    private new Rigidbody2D rigidbody;

    private MoveState moveState = MoveState.Walking;
    private enum MoveState
    {
        Walking,
        Sprinting,
        Crouching
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        controller = GetComponent<Controller>();
    }

    private void Update()
    {
        UpdateMoveState();

        switch (moveState)
        {
            case MoveState.Walking:
                maxSpeed = walkingMaxSpeed;
                maxAcceleration = walkingMaxAcceleration;
                break;
            case MoveState.Sprinting:
                maxSpeed = walkingMaxSpeed * sprintMultiplier;
                maxAcceleration = walkingMaxAcceleration * sprintMultiplier;
                break;
            case MoveState.Crouching:
                maxSpeed = walkingMaxSpeed * crouchMultiplier;
                maxAcceleration = walkingMaxAcceleration * crouchMultiplier;
                break;
        }

        direction.x = controller.input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.Friction, 0f);

        LookDirection();
    }

    private void FixedUpdate()
    {
        onGround = ground.OnGround;
        currentVelocity = rigidbody.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredVelocity.x, maxSpeedChange);

        rigidbody.velocity = currentVelocity;
    }

    private void LookDirection()
    {
        if (direction.x > 0f)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (direction.x < 0f)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    private void UpdateMoveState()
    {
        bool isCrouching = controller.input.RetrieveCrouchInput();
        bool isSprinting = controller.input.RetrieveSprintInput();
        if (!isCrouching && !isSprinting)
        {
            moveState = MoveState.Walking;
        }
        else if (isCrouching)
        {
            moveState = MoveState.Crouching;
        }
        else if (isSprinting)
        {
            moveState = MoveState.Sprinting;
        }
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public void SetMaxSpeed(float speed)
    {
        Debug.Log("Set speed: " + speed);
        maxSpeed = speed;
    }

    public float GetMaxAcceleration()
    {
        return maxAcceleration;
    }

    public void SetMaxAcceleration(float acceleration)
    {
        maxAcceleration = acceleration;
    }
}
