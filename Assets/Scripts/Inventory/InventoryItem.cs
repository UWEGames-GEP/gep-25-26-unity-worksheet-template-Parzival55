using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;

    public InventoryItem(Item item)
    {
        this.item = item;
        quantity = 1;
    }
}