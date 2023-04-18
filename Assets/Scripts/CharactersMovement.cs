using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMovement : MonoBehaviour
{
    float horizontal;
    new Rigidbody2D rigidbody;

    public float walkSpeed = 225f;
    public float jumpSpeed = 32f;
    public Transform groundCheck;
    public LayerMask groundLayerMask;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.freezeRotation = true;
    }

    /// <summary> Make the player jump at a certain speed. </summary>
    protected virtual void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        }

        if (rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
        }
    }

    /// <summary> Make the player look left if moving at -x direction and right at +x direction. </summary>
    protected void FlipPlayerEnabled()
    {
        if (horizontal > 0f)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (horizontal < 0f)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    /// <summary> Check player is on the ground before making a jump. </summary>
    /// <returns> True iff the player is on the ground.</returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayerMask);
    }
}
