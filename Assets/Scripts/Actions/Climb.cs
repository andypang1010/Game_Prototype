using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public float climbSpeed = 5f;

    private bool onLadder;
    private bool climbing;

    private Controller controller;
    private InteractionCheck ladderCheck;
    private Ground ground;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        ladderCheck = GetComponent<InteractionCheck>();
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        onLadder = ladderCheck.OnLadder;
        bool onGround = ground.OnGround;

        if (onLadder)
        {
            ClimbAction();
        }
    }

    private void ClimbAction()
    {
        float vertical = controller.input.RetrieveClimbInput();

        if (vertical != 0)
        {
            body.velocity = new Vector2(body.velocity.x, vertical * climbSpeed);
        }
        else
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }
}
