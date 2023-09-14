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

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0f);
        player.SetPosition(new Vector2(player.GetLadderObject().transform.position.x, player.gameObject.transform.position.y));
        player.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.rigidbody.gravityScale = 0f;

    }

    public override void Exit()
    {
        base.Exit();

        player.rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.rigidbody.gravityScale = playerData.gravityScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0) {
            player.CheckIfShouldFlip(xInput);
        }

        if (yInput < 0f && isGrounded || yInput > 0f && hasCeiling)
        {
            stateMachine.ChangeState(player.idleState);
        }

        else if (yInput != 0f)
        {
            stateMachine.ChangeState(player.climbMoveState);
        }

        else if (jumpInput) {
            stateMachine.ChangeState(player.jumpState);
        }

        else
        {
            player.SetVelocityY(0f);
        }
    }
}
