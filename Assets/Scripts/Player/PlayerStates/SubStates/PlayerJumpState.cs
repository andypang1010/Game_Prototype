using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;


    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        float currentJumpVelocity;

        float slope = (playerData.sprintJumpVelocity - playerData.jumpVelocity) / (playerData.sprintMaxSpeed - playerData.moveMaxSpeed);
        currentJumpVelocity = Mathf.Abs(player.currentVelocity.x) * slope + (playerData.jumpVelocity - slope * playerData.moveMaxSpeed);

        currentJumpVelocity = Mathf.Clamp(currentJumpVelocity, playerData.jumpVelocity, playerData.sprintJumpVelocity);
        Debug.Log(currentJumpVelocity);

        player.SetVelocityY(currentJumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
