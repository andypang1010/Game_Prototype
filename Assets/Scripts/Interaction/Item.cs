using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool isStackable = true;
    public int maxCount = 1;

    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using" + itemName);
    }
}
