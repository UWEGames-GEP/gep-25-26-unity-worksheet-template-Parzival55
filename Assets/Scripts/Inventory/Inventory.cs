using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 4;
    [SerializeField] private float maxWeight = 20f;

    public List<InventoryItem> items = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool AddItem(Item item)
    {
        //Weight check + UI feedback
        if (GetCurrentWeight() + item.weight > maxWeight)
        {
            Debug.Log("Inventory is too heavy!");

            if (InventoryUI.Instance != null)
            {
                InventoryUI.Instance.ShowInventoryTooHeavy();
            }

            return false;
        }
        // Checking if item already exists (stack it)
        InventoryItem inventoryItem = items.Find(i => i.item == item);

        if (inventoryItem != null)
        {
            inventoryItem.quantity++;
        }
        else
        {
            // Inventory full?
            if (items.Count >= maxSlots)
            {
                Debug.Log("Inventory Full!");

                if (InventoryUI.Instance != null)
                {
                    InventoryUI.Instance.ShowInventoryFull();
                }

                return false;
            }

            items.Add(new InventoryItem(item));
        }

        PrintInventory();

        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshInventory();
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        InventoryItem inventoryItem = items.Find(i => i.item == item);

        if (inventoryItem == null)
            return;

        inventoryItem.quantity--;

        if (inventoryItem.quantity <= 0)
        {
            items.Remove(inventoryItem);
        }

        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshInventory();
        }
    }

    public void PrintInventory()
    {
        foreach (InventoryItem item in items)
        {
            Debug.Log($"{item.item.itemName} x{item.quantity}");
        }
    }

    public void SortItemsAlphabetically()
    {
        items.Sort((a, b) => a.item.itemName.CompareTo(b.item.itemName));

        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshInventory();
        }
    }

    public bool IsInventoryFull()
    {
        return items.Count >= maxSlots;
    }

    public int GetCurrentSlots()
    {
        return items.Count;
    }

    public int GetMaxSlots()
    {
        return maxSlots;
    }

    private float GetCurrentWeight()
    {
        float totalWeight = 0f;

        foreach (InventoryItem item in items)
        {
            totalWeight += item.item.weight * item.quantity;
        }

        return totalWeight;
    }
}