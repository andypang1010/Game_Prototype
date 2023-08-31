using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbIdleState : PlayerAbilityState
{
    public PlayerClimbIdleState(
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

        if (yInput < 0f && isGrounded || yInput > 0f && hasCeiling)
        {
            stateMachine.ChangeState(player.idleState);
        }

        else if (yInput != 0f)
        {
            stateMachine.ChangeState(player.climbMoveState);
        }

        else
        {
            player.SetVelocityY(0f);
        }
    }
}
