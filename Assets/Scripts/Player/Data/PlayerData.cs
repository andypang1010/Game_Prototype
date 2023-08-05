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
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Crouch State")]
    public float crouchMaxSpeed = 6f;
    public float crouchMaxAcceleration = 20f;

    [Header("Sprint State")]
    public float sprintMaxSpeed = 15f;
    public float sprintMaxAcceleration = 45f;

    [Header("Check Variables")]
    public float groundCheckRadius;
    public LayerMask ground;
}
