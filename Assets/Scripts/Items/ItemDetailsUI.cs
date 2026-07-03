using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour

{
    public static ItemDetailsUI Instance { get; private set; }

    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text value;
    [SerializeField] private TMP_Text weight;
    [SerializeField] private TMP_Text rarity;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowItem(Item item)
    {
        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;
        description.text = item.description;
        value.text = $"Value: {item.value}";
        weight.text = $"Weight: {item.weight}";
        rarity.text = $"Rarity: {item.rarity}";

        itemIcon.enabled = true;

        switch (item.rarity)
        {
            case ItemRarity.Common:
                rarity.color = Color.white;
                break;

            case ItemRarity.Uncommon:
                rarity.color = Color.green;
                break;

            case ItemRarity.Rare:
                rarity.color = Color.cyan;
                break;

            case ItemRarity.Epic:
                rarity.color = new Color(0.7f, 0.2f, 1f);
                break;
        }
    }

    public void ClearDetails()
    {
        itemIcon.sprite = null;
        itemIcon.enabled = false;

        itemName.text = "No Item Selected";
        description.text = "Select an item to view its details.";
        value.text = "Value:";
        weight.text = "Weight:";
        rarity.text = "Rarity:";
    }
}