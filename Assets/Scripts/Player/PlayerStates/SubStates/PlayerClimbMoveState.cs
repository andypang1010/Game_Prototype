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

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0f);
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.SetPosition(new Vector2(player.GetLadderObject().transform.position.x, player.gameObject.transform.position.y));
        player.rigidbody.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();

        // TODO: Refactor to make more robust
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.rigidbody.gravityScale = playerData.gravityScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (jumpInput) {
            stateMachine.ChangeState(player.jumpState);
        }

        else if (yInput != 0f && player.CheckIfHasLadder())
        {
            player.SetVelocityY(yInput * playerData.climbSpeed);
        } 
        
        else if (!player.CheckIfHasLadder()) {
            stateMachine.ChangeState(player.idleState);
        }

        else
        {
            stateMachine.ChangeState(player.climbIdleState);
        }
    }
}
