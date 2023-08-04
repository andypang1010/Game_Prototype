using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    )
        : base(player, stateMachine, playerData, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(
                player.CalculateVelocityX(xInput, playerData.crouchMultiplier * playerData.walkingMaxSpeed, playerData.crouchMultiplier * playerData.walkingMaxAcceleration)
            );

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (yInput == 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}
