using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float maxSpeed = 5f;
    [SerializeField, Range(0, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction, desiredVelocity, currentVelocity;
    private float maxSpeedChange, acceleration;
    private bool onGround;

    private Ground ground;
    private Controller controller;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        controller = GetComponent<Controller>();
    }

    private void Update()
    {
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

    public float GetMaxSpeed() {
        return maxSpeed;
    }

    public void SetMaxSpeed(float speed) {
        maxSpeed = speed;
    }

        public float GetMaxAcceleration() {
        return maxAcceleration;
    }

    public void SetMaxAcceleration(float acceleration) {
        maxAcceleration = acceleration;
    }
}
