using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput,
        yInput;

    private bool jumpInput,
        crouchInput,
        sprintInput;

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
    }

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

        xInput = player.inputHandler.NormalizedInputX;
        yInput = player.inputHandler.NormalizedInputY;

        jumpInput = player.inputHandler.JumpInput;
        crouchInput = player.inputHandler.CrouchInput;
        sprintInput = player.inputHandler.SprintInput;

        if (jumpInput)
        {
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (crouchInput)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.CrouchMoveState);
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
