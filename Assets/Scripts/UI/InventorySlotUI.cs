using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private Button dropButton;

    [SerializeField] private AudioClip dropSound;

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
        dropPosition.y += 0.75f;

        //Instantiate(currentItem.item.prefab, dropPosition, currentItem.item.prefab.transform.rotation); - Fix from item prebad spawning rotations

        GameObject droppedItem = Instantiate(
        currentItem.item.prefab,
        dropPosition,
        Quaternion.identity
        );

        if (currentItem.item.itemName == "Wood")
        {
            droppedItem.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        if (dropSound != null)
        {
            AudioSource.PlayClipAtPoint(dropSound, dropPosition);
        }

        Inventory.Instance.RemoveItem(currentItem.item);
        InventoryUI.Instance.RefreshInventory();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemDetailsUI.Instance.ShowItem(currentItem.item);
    }
}