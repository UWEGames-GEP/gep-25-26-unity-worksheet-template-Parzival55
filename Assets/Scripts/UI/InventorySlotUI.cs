using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private Button dropButton;

    private InventoryItem currentItem;

    public void Setup(InventoryItem inventoryItem)
    {
        currentItem = inventoryItem;

        icon.sprite = inventoryItem.item.icon;
        itemName.text = inventoryItem.item.itemName;
        quantity.text = $"x{inventoryItem.quantity}";

        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(DropItem);
    }

    private void DropItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 dropPosition = player.transform.position;
        dropPosition += player.transform.forward * 1.0f;
        dropPosition.y += 0.2f;

        Instantiate(currentItem.item.prefab, dropPosition, Quaternion.identity);

        Inventory.Instance.RemoveItem(currentItem.item);
        InventoryUI.Instance.RefreshInventory();
    }
}