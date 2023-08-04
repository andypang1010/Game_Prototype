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

        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(
            player.CalculateVelocityX(
                xInput,
                playerData.moveMaxSpeed,
                playerData.moveMaxAcceleration
            )
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
