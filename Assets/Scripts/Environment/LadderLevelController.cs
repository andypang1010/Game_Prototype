using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderLevelController : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Level");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (player.GetComponent<Player>().stateMachine.GetState() == "PlayerClimbMoveState" 
        || player.GetComponent<Player>().stateMachine.GetState() == "PlayerClimbIdleState") {
                gameObject.layer = LayerMask.NameToLayer("TransparentLevel");
        }
        else {
            gameObject.layer = LayerMask.NameToLayer("Level");
        }
    }
}
