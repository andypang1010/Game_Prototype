using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput, yInput;

    private bool jumpInput, crouchInput, sprintInput;

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

        xInput = player.InputHandler.NormalizedInputX;
        yInput = player.InputHandler.NormalizedInputY;

        jumpInput = player.InputHandler.JumpInput;
        crouchInput = player.InputHandler.CrouchInput;
        sprintInput = player.InputHandler.SprintInput;

        if (jumpInput)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }

        else if (crouchInput) {
            if (xInput == 0) {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else {
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
