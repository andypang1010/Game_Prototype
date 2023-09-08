using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public int pickupCount = 1;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        bool wasPickedUp = Inventory.Instance.Add(item, pickupCount);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
