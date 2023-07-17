using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new CapsuleCollider2D collider;
    new Rigidbody2D rigidbody;
    float horizontal;
    float vertical;

    bool isStealth,
        isSprinting,
        startJumping,
        onLadder;

    float defaultGravityScale = 1.0f;

    [Header("Actions")]
    [SerializeField]
    bool movementEnabled = true;

    [SerializeField]
    bool sprintEnabled = true;

    [SerializeField]
    bool crouchEnabled = true;

    [SerializeField]
    bool jumpEnabled = true;

    [SerializeField]
    bool climbEnabled = true;

    [Header("Movement")]
    public float walkSpeed = 225f;
    public float sprintSpeedMultiplier = 1.85f;

    [Header("Stealth")]
    public float stealthSize = 0.5f;
    public float stealthSpeedMultiplier = 0.5f;
    public Transform ceilingCheck;

    private bool canCrouch;

    [Header("Jump")]
    public float jumpSpeed = 32f;
    public Transform groundCheck;
    public LayerMask groundLayerMask;

    [Header("Climb")]
    public float climbSpeed = 5f;

    [HideInInspector]
    public bool stealthMoving,
        sprintMoving;

    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;
    }

    void Update()
    {
        // Get user keyboard input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isStealth = Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl);
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        startJumping = Input.GetButtonDown("Jump");

        // Check if player is in stealth/sprint state and is moving
        stealthMoving = isStealth && Mathf.Abs(horizontal) > 0f;
        sprintMoving = isSprinting && Mathf.Abs(horizontal) > 0f;

        FlipPlayerDirection();

        if (startJumping)
        {
            canCrouch = false;
            Jump();
            canCrouch = true;
        }

        if (onLadder)
        {
            jumpEnabled = false;
            Climb();
        }
        else
        {
            jumpEnabled = true;
        }

        if ((isStealth || (HasCeiling() && IsGrounded())) && canCrouch)
        {
            startJumping = false;
            isStealth = true;
            Crouch();
        }
        else if (!isStealth || !HasCeiling())
        {
            Stand();
        }
    }

    private void FixedUpdate()
    {
        float moveSpeed = 0;
        if (movementEnabled)
        {
            moveSpeed = walkSpeed;
        }

        if (isStealth && crouchEnabled)
            moveSpeed *= stealthSpeedMultiplier;
        else if (isSprinting && sprintEnabled)
            moveSpeed *= sprintSpeedMultiplier;

        rigidbody.velocity = new Vector2(
            horizontal * Time.deltaTime * moveSpeed,
            rigidbody.velocity.y
        );
    }

    /// <summary> Make the player crouch down. </summary>
    private void Crouch()
    {
        if (crouchEnabled)
        {
            transform.localScale = new Vector3(1, stealthSize, 1);
            collider.transform.localScale = new Vector3(1, stealthSize, 1);
        }
    }

    /// <summary> Make the player stand up at idle scale. </summary>
    private void Stand()
    {
        transform.localScale = new Vector3(1, 1, 1);
        collider.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary> Make the player look left if moving at -x direction and right at +x direction. </summary>
    private void FlipPlayerDirection()
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

    /// <summary> Jump </summary>
    private void Jump()
    {
        if (jumpEnabled == true)
        {
            if (IsGrounded())
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            }

            if (rigidbody.velocity.y > 0f)
            {
                isSprinting = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
            }
        }
    }

    /// <summary> Climb ladder </summary>
    private void Climb()
    {
        if (climbEnabled)
        {
            if (Mathf.Abs(vertical) > 0f)
            {
                print("moving on ladder");
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertical * climbSpeed);
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
        }
    }

    /// <summary> Check player is on the ground before making a jump. </summary>
    /// <returns> True iff the player is on the ground.</returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayerMask);
    }

    /// <summary> Check if player has ceiling when standing up. </summary>
    /// <returns> True iff the player has a ceiling above.</returns>
    private bool HasCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, 1 - stealthSize, groundLayerMask);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
        }
        else
        {
            onLadder = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}
