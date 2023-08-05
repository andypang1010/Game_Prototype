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
                player.CalculateVelocityX(
                    xInput,
                    playerData.crouchMaxSpeed,
                    playerData.crouchMaxAcceleration
                )
            );

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else if (!crouchInput)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
