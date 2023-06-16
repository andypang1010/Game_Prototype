using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public bool initialFaceRight = true;
    public float stoppingDistance = 2f;

    private float pathRefreshRate = 0.3f;
    private float nextWaypointDistance = 2f;

    [Header("Movement")]
    public float walkSpeed = 200f;
    public float climbSpeed = 10f;
    private float minSprintingDistance = 5f;
    public float sprintSpeedMultiplier = 1.85f;

    [Header("Jump")]
    public float jumpSpeed = 45f;

    [Range(0, 360)]
    public float minJumpAngle = 50f;
    public Transform groundCheck;
    public LayerMask groundLayerMask;

    private int currentWaypoint = 0;

    private bool jumpEnabled = true;
    private bool onLadder;
    private Vector2 playerDirection;
    private Path path;
    private Vector2 nextWaypointDirection;

    new Rigidbody2D rigidbody;
    new Collider2D collider;
    Seeker seeker;
    EnemyFOV fov;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        seeker = GetComponent<Seeker>();
        fov = GetComponent<EnemyFOV>();

        transform.rotation = new Quaternion(0, initialFaceRight ? 0 : 180, 0, 0);

        InvokeRepeating("UpdatePath", 0f, pathRefreshRate);
    }

    private void Update()
    {
        UpdateLookDirection();

        if (path == null)
            return;

        // Find the vector direction of the next waypoint
        nextWaypointDirection = (
            path.vectorPath[currentWaypoint] - transform.position
        ).normalized;

        float nextWaypointAngle =
            Mathf.Atan(nextWaypointDirection.y / nextWaypointDirection.x) * Mathf.Rad2Deg;

        // Jump if the angle is greater than minimum jump angle
        if ((nextWaypointAngle > minJumpAngle || nextWaypointAngle < -minJumpAngle) && jumpEnabled)
        {
            Jump();
        }
        Debug.DrawLine(path.vectorPath[currentWaypoint], path.vectorPath[currentWaypoint] + Vector3.up);
    }

    private void FixedUpdate()
    {
        // If no path exists, do nothing
        if (path == null)
        {
            return;
        }

        // If reached the end of the path, stop moving
        if (currentWaypoint >= path.vectorPath.Count)
        {
            DisableMovement();
            return;
        }

        /* If the distance between current position and the current waypoint
         * position is smaller than the next waypoint distance, set the next
         * waypoint as the new waypoint */
        float waypointDistance = Vector2.Distance(
            rigidbody.position,
            path.vectorPath[currentWaypoint]
        );
        if (waypointDistance <= nextWaypointDistance)
        {
            currentWaypoint = Mathf.Min(currentWaypoint + 1, path.vectorPath.Count - 1);
        }

        // Calculate direction from current position to the player
        playerDirection = (
            (Vector2)path.vectorPath[currentWaypoint] - rigidbody.position
        ).normalized;

        float playerDistance = (target.position - transform.position).magnitude;

        // If the distance between player and enemy is larger than the stopping distance, approach the player
        if (playerDistance >= stoppingDistance)
        {
            if (onLadder && (nextWaypointDirection == Vector2.up || nextWaypointDirection == Vector2.down)) {
                ClimbLadder();
            }
            else {
            jumpEnabled = true;

            // Move the enemy: if larger than minimum sprinting distance, sprint towards the player
            rigidbody.velocity = new Vector2(
                Mathf.Sign(playerDirection.x)
                    * ((playerDistance >= minSprintingDistance) ? sprintSpeedMultiplier : 1)
                    * walkSpeed
                    * Time.deltaTime,
                rigidbody.velocity.y
            );
            }

        }
        else
        {
            DisableMovement();
        }
    }

    /// <summary> Stop the enemy from moving. </summary>
    private void DisableMovement()
    {
        jumpEnabled = false;
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }

    /// <summary> If player is seen and the current path has been computed, start calculating the next path. </summary>
    private void UpdatePath()
    {
        if (PlayerFound() && seeker.IsDone())
        {
            seeker.StartPath(rigidbody.position, target.position, OnPathCalculationComplete);
        }
    }

    /// <summary> If the path has been calculated and is not failed, set this path as the enemy path </summary>
    private void OnPathCalculationComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }

    /// <summary> Make the enemy look left if moving at -x direction and right at +x direction. </summary>
    private void UpdateLookDirection()
    {
        if (rigidbody.velocity.x > 0f)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (rigidbody.velocity.x < 0f)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    /// <summary> Check if player is within field of view.</summary>
    /// <returns> True iff the player can be seen or heard.</returns>
    private bool PlayerFound()
    {
        return fov.playerFound;
    }

    /// <summary> Make the enemy jump. </summary>
    private void Jump()
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

    /// <summary> Climb the ladder </summary>
    private void ClimbLadder() {
        if (nextWaypointDirection == Vector2.up)
            {
                rigidbody.velocity = new Vector2(0, climbSpeed);

            }

            else if (nextWaypointDirection == Vector2.down)
            {
                rigidbody.velocity = new Vector2(0, -climbSpeed);
            }
    }

    /// <summary> Check enemy is on the ground before making a jump. </summary>
    /// <returns> True iff the enemy is on the ground.</returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayerMask);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;   
        }

        else {
            onLadder = false;
        }
    }

}
