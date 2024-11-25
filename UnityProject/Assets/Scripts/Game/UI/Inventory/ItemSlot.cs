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
    [SerializeField] private Image selectionFrame; // Cadre pour indiquer la sélection

    private void Awake()
    {
        button.onClick.AddListener(OnItemClick);

        // Assurez-vous que le cadre de sélection est désactivé par défaut
        if (selectionFrame != null)
        {
            selectionFrame.enabled = false;
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
        // Trouver tous les slots d'item
        var allSlots = FindObjectsOfType<ItemSlot>();

        // Désactiver les cadres de sélection de tous les autres slots
        foreach (var slot in allSlots)
        {
            if (slot.selectionFrame != null)
            {
                slot.selectionFrame.enabled = false;
            }
        }

        // Activer le cadre de sélection pour ce slot
        if (selectionFrame != null)
        {
            selectionFrame.enabled = true;
        }
    }
}
