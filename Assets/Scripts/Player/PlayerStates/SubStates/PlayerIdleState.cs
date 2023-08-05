using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName
    )
        : base(player, stateMachine, playerData, animBoolName) { }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0)
        {
<<<<<<< Updated upstream
            stateMachine.ChangeState(player.moveState);
=======
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if (crouchInput)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
>>>>>>> Stashed changes
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
