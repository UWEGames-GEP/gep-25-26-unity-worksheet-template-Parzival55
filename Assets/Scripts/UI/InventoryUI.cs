using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform content;
    [SerializeField] private InventorySlotUI itemPrefab;

    private bool isOpen;

    private void Awake()
    {
        Instance = this;
    }

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

    public void RefreshInventory()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in Inventory.Instance.items)
        {
            Debug.Log($"Creating slot for {item.item.itemName}");

            InventorySlotUI slot = Instantiate(itemPrefab, content);
            slot.Setup(item);
        }
    }
}