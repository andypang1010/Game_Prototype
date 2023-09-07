using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Pick up");
        bool wasPickedUp = Inventory.Instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
