using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    )
        : base(player, stateMachine, playerData, animBoolName) { }

    private Vector2 desiredVelocity,
        currentVelocity;
    private float maxSpeed,
        maxAcceleration,
        maxSpeedChange,
        acceleration;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        maxSpeed = playerData.walkingMaxSpeed;
        maxAcceleration = playerData.walkingMaxAcceleration;

        player.CheckIfShouldFlip(xInput);
        desiredVelocity = new Vector2(xInput, 0f) * maxSpeed;

        currentVelocity = player.currentVelocity;
        acceleration = maxAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;

        player.SetVelocityX(
            Mathf.MoveTowards(currentVelocity.x, desiredVelocity.x, maxSpeedChange)
        );

        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(player.crouchMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
