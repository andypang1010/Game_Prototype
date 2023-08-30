using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    )
        : base(player, stateMachine, playerData, animBoolName) { }

    public override void LogicUpdate()
    {
        if (xInput != 0)
        {
            if (sprintInput)
            {
                stateMachine.ChangeState(player.sprintState);
            }
            else
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
        else if (isAnimationFinished)
        { 
            stateMachine.ChangeState(player.idleState);
        }

        base.LogicUpdate();
    }
}
