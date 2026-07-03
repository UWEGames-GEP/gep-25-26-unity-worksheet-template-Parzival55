using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform content;
    [SerializeField] private InventorySlotUI itemPrefab;
    [SerializeField] private Button sortButton;
    [SerializeField] private TMP_Text totalWeightText;
    [SerializeField] private TMP_Text itemCountText;

    private bool isOpen;

    private void Awake()
    {
        Instance = this;

        Debug.Log("InventoryUI Awake");

        sortButton.onClick.AddListener(() =>
        {
            Debug.Log("BUTTON CLICKED");
            SortInventory();
        });
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

        UpdateTotalWeight();
        UpdateItemCount();

        if (Inventory.Instance.items.Count == 0)
        {
            ItemDetailsUI.Instance.ClearDetails();
        }

        sortButton.interactable = Inventory.Instance.items.Count > 1;
    }

    public void SortInventory()
    {
        Debug.Log("Sort button clicked!");

        Inventory.Instance.SortItemsAlphabetically();
        RefreshInventory();
    }

    private void UpdateTotalWeight()
    {
        float totalWeight = 0;

        foreach (InventoryItem item in Inventory.Instance.items)
        {
            totalWeight += item.item.weight * item.quantity;
        }

        totalWeightText.text = $"Weight: {totalWeight:0.0} kg";
    }

    private void UpdateItemCount()
    {
        int totalItems = 0;

        foreach (InventoryItem item in Inventory.Instance.items)
        {
            totalItems += item.quantity;
        }

        itemCountText.text = $"Items: {totalItems}";
    }
}