using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput,
        yInput;

    protected bool jumpInput,
        crouchInput,
        sprintInput;
    protected bool isGrounded, hasLadder;

    public PlayerGroundedState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    )
        : base(player, stateMachine, playerData, animBoolName) { }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        hasLadder = player.CheckIfHasLadder();
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.normalizedInputX;
        yInput = player.inputHandler.normalizedInputY;

        jumpInput = player.inputHandler.jumpInput;
        crouchInput = player.inputHandler.crouchInput;
        sprintInput = player.inputHandler.sprintInput;

        if (hasLadder && yInput != 0f)
        {
            stateMachine.ChangeState(player.climbIdleState);
        }
        else if (jumpInput && player.jumpState.CanJump() && !crouchInput)
        {
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
    }
}

