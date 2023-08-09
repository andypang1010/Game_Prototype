using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerGroundedState
{
    public PlayerSprintState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
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

        if (!isExitingState)
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(
                player.CalculateVelocityX(
                    xInput,
                    playerData.sprintMaxSpeed,
                    playerData.sprintMaxAcceleration
                )
            );
            

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if (!sprintInput)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
