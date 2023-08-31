using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float moveMaxSpeed = 10f;
    public float moveMaxAcceleration = 35f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public float sprintJumpVelocity = 25f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Crouch State")]
    public float crouchMaxSpeed = 6f;
    public float crouchMaxAcceleration = 20f;
    public float crouchSizeMultiplier = 0.6f;

    [Header("Sprint State")]
    public float sprintMaxSpeed = 15f;
    public float sprintMaxAcceleration = 45f;

    [Header("Climb State")]
    public float climbSpeed = 10f;

    [Header("Check Variables")]
    public float groundCheckRadius;
    public LayerMask ground;
    public float ladderCheckRadius;
    public LayerMask ladder;
}
