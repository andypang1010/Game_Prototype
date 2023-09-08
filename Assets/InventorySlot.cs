using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text itemCountText;

    private Item item;

    // Show the item in the slot
    public void ShowItem(Item newItem)
    {
        item = newItem;

        // UI changes
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        int curCount = Inventory.Instance.itemsCount[newItem];

        // only show count when there's more than 1 item
        itemCountText.text = curCount > 1 ? curCount.ToString() : "";

        // set the text color to red when max count is reached
        if (curCount == item.maxCount)
        {
            itemCountText.color = Color.red;
        }
        else
        {
            itemCountText.color = Color.white;
        }
    }

    public void ClearSlot()
    {
        item = null;

        // UI changes
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        itemCountText.text = "";
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.RemoveAll(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
