using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    private Transform player;

    private void Start()
    {
        // TODO: will not work with multiple players
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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
