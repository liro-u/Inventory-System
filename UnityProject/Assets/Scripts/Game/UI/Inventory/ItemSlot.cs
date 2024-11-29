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
    [SerializeField] private Image selectionFrame; 

    [Header("Selection Frame Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow; 

    private void Awake()
    {
        button.onClick.AddListener(OnItemClick);

        // S'assurer que tous les cadres utilisent la couleur par défaut au départ
        if (selectionFrame != null)
        {
            selectionFrame.color = defaultColor;
            selectionFrame.enabled = true; // Assurez-vous que le cadre est activé pour tous
        }
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
        // Afficher les détails de l'item dans l'inventaire
        Inventory inventory = (Inventory)UIManager.Instance.GetBaseUIByName("InventoryUI");
        inventory.ShowItemDetails(itemName, itemDescription, itemQuantity, itemId.maxQuantityPerSlot, sprite, itemId.rarity);

        // Définir cet item comme sélectionné
        SetSelected();
    }

    public void SetSelected()
    {
        var allSlots = FindObjectsOfType<ItemSlot>();

        foreach (var slot in allSlots)
        {
            if (slot.selectionFrame != null)
            {
                slot.selectionFrame.color = defaultColor;
            }
        }

        if (selectionFrame != null)
        {
            selectionFrame.color = selectedColor;
        }
    }
}
