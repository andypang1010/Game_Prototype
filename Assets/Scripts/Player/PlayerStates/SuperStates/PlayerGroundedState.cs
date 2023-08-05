using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

<<<<<<< Updated upstream
    private bool jumpInput;
    private bool isGrounded;
=======
    protected bool jumpInput,
        crouchInput,
        sprintInput;
>>>>>>> Stashed changes

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

        xInput = player.InputHandler.normalizedInputX;
        yInput = player.InputHandler.normalizedInputY;

        jumpInput = player.InputHandler.jumpInput;

        if (jumpInput && player.jumpState.CanJump())
        {
<<<<<<< Updated upstream
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
=======
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
>>>>>>> Stashed changes
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
