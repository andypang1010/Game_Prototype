using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput,
        yInput;

    protected bool jumpInput,
        crouchInput,
        sprintInput;
    protected bool isGrounded;

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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.normalizedInputX;
        yInput = player.inputHandler.normalizedInputY;

        jumpInput = player.inputHandler.jumpInput;
        crouchInput = player.inputHandler.crouchInput;
        sprintInput = player.inputHandler.sprintInput;

        if (jumpInput && player.jumpState.CanJump())
        {
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (crouchInput)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.crouchMoveState);
            }
        }

        // else if (sprintInput && xInput != 0) {
        //     stateMachine.ChangeState(player.sprintState);
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
