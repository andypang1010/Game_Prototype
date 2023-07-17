using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    private Collider2D collider;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;
    }

}
