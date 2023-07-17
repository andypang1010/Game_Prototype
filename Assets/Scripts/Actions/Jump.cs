using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    private Controller controller;
    private Ground ground;
    private Rigidbody2D body;

    bool onGround;
    bool desireJump;

    private int jumpState = 0;
    private Vector2 velocity;

    [Header("Jump")]
    public float jumpSpeed = 32f;
    public int maxAirJumps = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        controller = GetComponent<Controller>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        desireJump |= controller.input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        onGround = ground.OnGround;
        velocity = body.velocity;

        if (onGround)
        {
            jumpState = 0;
        }

        if (desireJump)
        {
            desireJump = false;
            JumpAction();
        }

        body.velocity = velocity;
    }

    /// <summary> Jump </summary>
    private void JumpAction()
    {
        if (onGround || jumpState < maxAirJumps)
        {
            jumpState++;

            velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    public bool GetIsJump()
    {
        return jumpState == 0;
    }
}
