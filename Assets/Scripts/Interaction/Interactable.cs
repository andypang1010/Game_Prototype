using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    private Transform playerInteractionCheck;

    private void Start()
    {
        // TODO: will not work with multiple players
        playerInteractionCheck = GameObject.FindGameObjectWithTag("Player").transform.Find("InteractionCheck");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerInteractionCheck.position);
        if (distance < radius)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
