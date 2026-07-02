using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemPrefab;

    private bool isOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;

        inventoryPanel.SetActive(isOpen);

        GameManager.Instance.SetState(isOpen ? GameState.InventoryOpen : GameState.Playing);

        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;

        if (isOpen)
        {
            RefreshInventory();
        }
    }

    private void RefreshInventory()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in Inventory.Instance.items)
        {
            GameObject uiItem = Instantiate(itemPrefab, content);

            TMP_Text text = uiItem.GetComponent<TMP_Text>();

            text.text = $"{item.item.itemName} x{item.quantity}";
        }
    }
}