using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCheck : MonoBehaviour
{
    public bool OnLadder { get; private set; }

    // Check for all overlapping interactions, eg
    // public bool OnChest { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EvaluateTrigger(collision, true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EvaluateTrigger(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EvaluateTrigger(collision, false);
    }

    private void EvaluateTrigger(Collider2D collision, bool overlapping)
    {
        switch (collision.tag)
        {
            case "Ladder":
                OnLadder = overlapping;
                break;
        }
    }
}
