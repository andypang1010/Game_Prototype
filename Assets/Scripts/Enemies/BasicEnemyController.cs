using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        moveSpeed,
        maxHealth,
        knockbackDuration;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Vector2 knockbackSpeed;

    private float 
        currentHealth,
        knockbackStartTime;

    private int 
        facingDirection,
        damageDirection;

    private Vector2 movement;

    private bool groundDetected,
        wallDetected;
    private GameObject alive;
    private Rigidbody2D aliveRB;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        aliveAnim = GetComponent<Animator>();

        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    #region Moving State
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer);

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(moveSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }
    #endregion

    #region Knockback State
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;

        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time > knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }
    #endregion

    #region Dead State
    private void EnterDeadState()
    {
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }
    #endregion

    #region Other Functions
    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        // Hit particles
        if(currentHealth > 0)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth < 0)
        {
            SwitchState(State.Dead);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0, 180f, 0);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion
}
