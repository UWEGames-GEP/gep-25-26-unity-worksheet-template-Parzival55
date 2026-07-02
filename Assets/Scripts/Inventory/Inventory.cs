using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> items = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(Item item)
    {
        InventoryItem inventoryItem = items.Find(i => i.item == item);

        if (inventoryItem != null)
        {
            inventoryItem.quantity++;
        }
        else
        {
            items.Add(new InventoryItem(item));
        }

        PrintInventory();
    }

    public void PrintInventory()
    {
        foreach (InventoryItem item in items)
        {
            Debug.Log($"{item.item.itemName} x{item.quantity}");
        }
    }
}