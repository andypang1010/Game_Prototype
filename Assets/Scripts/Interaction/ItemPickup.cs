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

        // TODO: allow picking up different number of items
        bool wasPickedUp = Inventory.Instance.Add(item, 1);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
