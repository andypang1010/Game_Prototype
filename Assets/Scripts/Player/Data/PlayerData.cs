using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float walkingMaxSpeed = 5f;
    public float walkingMaxAcceleration = 35f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;

    [Header("Crouch State")]
    public float crouchMultiplier = 0.65f;

    [Header("Sprint State")]
    public float sprintMultiplier = 1.8f;

    [Header("Check Variables")]
    public float groundCheckRadius;
    public LayerMask ground;
}
