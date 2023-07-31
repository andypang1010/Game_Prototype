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
            Debug.Log("calculated: " + player.currentVelocity.x * playerData.crouchMultiplier);
            player.SetVelocityX(player.currentVelocity.x * playerData.crouchMultiplier);
            player.CheckIfShouldFlip(xInput);

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else if (yInput == 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
