using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMoveState crouchMoveState { get; private set; }

    #endregion

    #region Components
    public Animator anim { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public new Rigidbody2D rigidbody { get; private set; }

    [SerializeField]
    private readonly PlayerData playerData;
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;

    #endregion

    #region Other Variables
    private Vector2 workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        // TODO: animation not yet created
        idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        landState = new PlayerLandState(this, stateMachine, playerData, "land");
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        rigidbody = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        CurrentVelocity = rigidbody.velocity;
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityZero()
    {
        workspace.Set(0, 0);
        rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            playerData.groundCheckRadius,
            playerData.ground
        );
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions

    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public float CalculateVelocityX(int xInput, float maxSpeed, float maxAcceleration)
    {
        Vector2 desiredVelocity = new Vector2(xInput, 0f) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        return Mathf.MoveTowards(CurrentVelocity.x, desiredVelocity.x, maxSpeedChange);
    }

    #endregion
}
