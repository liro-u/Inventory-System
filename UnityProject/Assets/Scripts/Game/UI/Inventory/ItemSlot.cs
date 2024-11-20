using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Item itemId;
    private string itemName;
    private string itemDescription;
    private int itemQuantity;
    private Sprite sprite;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private Image itemRarityBackgroundImage;


    private void Awake()
    {
        button.onClick.AddListener(OnItemClick);
    }

    public void Setup(UserItem userItem, int quantity)
    {
        itemId = userItem.itemId;
        itemName = userItem.itemId.name;
        itemDescription = userItem.itemId.description;
        itemQuantity = quantity;
        sprite = GameDataManager.Instance.ItemAdditionalData.GetAdditionalDataById(userItem.itemId._id).sprite;

        icon.sprite = sprite;
        quantityText.text = quantity.ToString();
        itemRarityBackgroundImage.color = GameDataManager.Instance.RarityAdditionalData.GetAdditionalDataByRarity(userItem.itemId.rarity).color;
    }

    public void OnItemClick()
    {
        Inventory inventory = (Inventory)UIManager.Instance.GetBaseUIByName("InventoryUI");
        inventory.ShowItemDetails(itemName, itemDescription, itemQuantity, itemId.maxQuantityPerSlot, sprite, itemId.rarity);
    }
}
