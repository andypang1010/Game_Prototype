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

    // return true when add was successful, otherwise return false
    public bool Add(Item item, int count)
    {
        // item can only be added when 
        // 1. there're enough slots
        // 2. picking up the item doesn't exceed the item max count
        if (items.Count < space && !(itemsCount.ContainsKey(item) && itemsCount[item] + count > item.maxCount))
        {
            // add item to list only when it doesn't already exist or its count is 0 (not in inventory)
            if (!itemsCount.ContainsKey(item) || itemsCount[item] == 0)
            {
                items.Add(item);
            }

            // handle count update differently depending on whether the item already exists or not
            if (itemsCount.ContainsKey(item))
            {
                itemsCount[item] = Mathf.Min(itemsCount[item] + count, item.maxCount);
            }
            else
            {
                itemsCount[item] = Mathf.Min(count, item.maxCount);
            }

            // invoke callback functions when an item has changed
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
            itemsCount.Remove(item);
        }
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemoveAll(Item item)
    {
        Remove(item, itemsCount[item]);
    }
}
