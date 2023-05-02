using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    // [HideInInspector]
    public bool playerFound;
    public float stealthHearingRadius = 5f;
    public float walkingHearingRadius = 10f;
    public float runningHearingRadius = 15f;
    public float viewRadius = 15f;

    [Range(0, 360)]
    public float viewAngle = 55f;

    public GameObject player;

    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    LayerMask obstructionMask;

    void Start()
    {
        StartCoroutine(CheckFOV());
    }

    IEnumerator CheckFOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        // Check if player exists within field
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(
            transform.position,
            viewRadius,
            targetMask
        );

        if (rangeChecks.Length > 0)
        {
            Transform targetTransform = rangeChecks[0].transform;

            // Check normal direction of player
            Vector2 targetDirection = (targetTransform.position - transform.position).normalized;

            // Check distance to player
            float targetDistance = Vector2.Distance(transform.position, targetTransform.position);

            if (
                (
                    // If player is on stealth mode and is within stealth hearing radius
                    targetDistance < stealthHearingRadius
                    && player.GetComponent<PlayerMovement>().stealthMoving
                )
                || (
                    // If player is on walking / climbing mode and is within walking hearing radius
                    targetDistance < walkingHearingRadius
                    && (
                        (
                            !player.GetComponent<PlayerMovement>().sprintMoving
                            && !player.GetComponent<PlayerMovement>().stealthMoving
                        )
                    )
                )
                || (
                    // If player is on running mode and is within running hearing radius
                    targetDistance < runningHearingRadius
                    && player.GetComponent<PlayerMovement>().sprintMoving
                )
            )
            {
                playerFound = true;
            }
            else if (Vector2.Angle(transform.right, targetDirection) < (viewAngle / 2))
            {
                // If no obstructing game object exists between player and enemy, then player is found
                if (
                    !Physics2D.Raycast(
                        transform.position,
                        targetDirection,
                        targetDistance,
                        obstructionMask
                    )
                )
                {
                    playerFound = true;
                }
                else
                {
                    playerFound = false;
                }
            }
            else
            {
                playerFound = false;
            }
        }
        else
        {
            playerFound = false;
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, runningHearingRadius);

    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, walkingHearingRadius);

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, stealthHearingRadius);
    // }
}
