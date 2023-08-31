using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbMoveState : PlayerAbilityState
{
    public PlayerClimbMoveState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    )
        : base(player, stateMachine, playerData, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(0f);

        if (yInput != 0f)
        {
            player.SetVelocityY(yInput * playerData.climbSpeed);
        }

        else
        {
            stateMachine.ChangeState(player.climbIdleState);
        }
    }
}
