using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // the max number of slots
    public int space = 9;

    public List<Item> items { private set; get; } = new List<Item>();
    public Dictionary<Item, int> itemsCount { private set; get; } = new Dictionary<Item, int>();

    public bool Add(Item item, int count)
    {
        if (items.Count < space && !(itemsCount.ContainsKey(item) && itemsCount[item] == item.maxCount))
        {
            if (!itemsCount.ContainsKey(item) || itemsCount[item] == 0)
            {
                items.Add(item);
            }
            if (itemsCount.ContainsKey(item))
            {
                // note if items count exceeds the max count, the excess items will disappear
                itemsCount[item] = Mathf.Min(itemsCount[item] + count, item.maxCount);
            }
            else
            {
                itemsCount[item] = Mathf.Min(count, item.maxCount);
            }
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Remove(Item item, int count)
    {
        itemsCount[item] = Mathf.Max(itemsCount[item] - count, 0);
        if (itemsCount[item] == 0)
        {
            items.Remove(item);
        }
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemoveAll(Item item)
    {
        Remove(item, itemsCount[item]);
    }
}
