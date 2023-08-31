using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected int yInput;
    protected bool isAbilityDone, isGrounded, hasLadder, hasCeiling;

    public PlayerAbilityState(
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
        hasLadder = player.CheckIfHasLadder();
        hasCeiling = player.CheckIfHasCeiling();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        yInput = player.inputHandler.normalizedInputY;

        if (isAbilityDone)
        {
            if (isGrounded)
            {
                if (player.currentVelocity.y < 0.001f)
                {
                    stateMachine.ChangeState(player.idleState);
                }
                else
                {
                    stateMachine.ChangeState(player.inAirState);
                }
            }

            else if (hasLadder)
            {
                if (Mathf.Abs(player.currentVelocity.y) > 0f) 
                {
                    stateMachine.ChangeState(player.climbMoveState);
                }
                else
                {
                    stateMachine.ChangeState(player.climbIdleState);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
