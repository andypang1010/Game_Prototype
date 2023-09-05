using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    Transform player;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
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
