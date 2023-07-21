using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion

<<<<<<< HEAD
    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public new Rigidbody2D rigidbody { get; private set; }

=======
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3
    [SerializeField]
    private PlayerData playerData;
    #endregion

<<<<<<< HEAD
    #region Other Variables
    private Vector2 workspace;
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }
    #endregion

=======
    #region Component Variables
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;
    #endregion
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        // TODO: animation not yet created
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
<<<<<<< HEAD
        rigidbody = GetComponent<Rigidbody2D>();

        facingDirection = 1;
=======
        RB = GetComponent<Rigidbody2D>();

        FacingDirection = 1;
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
<<<<<<< HEAD
        currentVelocity = rigidbody.velocity;
=======
        CurrentVelocity = RB.velocity;
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
<<<<<<< HEAD
        workspace.Set(velocity, currentVelocity.y);
        rigidbody.velocity = workspace;
        currentVelocity = workspace;
=======
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
<<<<<<< HEAD
        if (xInput != 0 && xInput != facingDirection)
=======
        if(xInput != 0 && xInput != FacingDirection)
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions
    private void Flip()
    {
<<<<<<< HEAD
        facingDirection *= -1;
=======
        FacingDirection *= -1;
>>>>>>> 39c4b7feb5e2de1ec9bbc95537bc3dbc80f0ade3
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
}
