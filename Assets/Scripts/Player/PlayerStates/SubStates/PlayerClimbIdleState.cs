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
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.SetPosition(new Vector2(player.GetLadderObject().transform.position.x, player.gameObject.transform.position.y));

    }

    public override void Exit()
    {
        base.Exit();

        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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
