using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : BaseUI
{
    [Header("Inventory Grid")]
    public GameObject inventoryGrid;
    public GameObject ItemSlotPrefab;

    [Header("Inventory Detail")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemQuantityText;
    public Image itemIconImage;

    private bool hasSimulateClickOnFirst = false;

    private void Start()
    {
        GameDataManager.Instance.InventoryData.OnUserInventoryChange += UpdateInventoryGrid;
        UpdateInventoryGrid();
    }

    private void OnEnable()
    {
        UpdateInventoryGrid();
    }

    private void UpdateInventoryGrid()
    {
        // Effacer tous les éléments enfants existants dans la grille d'inventaire
        foreach (Transform child in inventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }

        if (GameDataManager.Instance.InventoryData != null)
        {
            // Get the user inventory from GameDataManager
            var userInventory = GameDataManager.Instance.InventoryData.UserInventory;

            // Loop through the inventory list and create a slot for each item
            foreach (var userItem in userInventory)
            {
                var quantity = userItem.quantity;
                var numberOfSlots = (int)Mathf.Ceil((float)quantity / userItem.itemId.maxQuantityPerSlot);
                if (numberOfSlots > 0)
                {
                    for (var i = 0; i < numberOfSlots; i++)
                    {
                        int currentQuantity = quantity > userItem.itemId.maxQuantityPerSlot ? userItem.itemId.maxQuantityPerSlot : quantity;

                        CreateItemSlot(userItem, currentQuantity);

                        quantity -= userItem.itemId.maxQuantityPerSlot;
                    }
                }
                else
                {
                    CreateItemSlot(userItem, quantity);
                }
            }
        }
    }

    private void CreateItemSlot(UserItem userItem, int quantity)
    {
        var newSlot = Instantiate(ItemSlotPrefab, inventoryGrid.transform);

        ItemSlot slotComponent = newSlot.GetComponent<ItemSlot>();
        if (slotComponent != null)
        {
            slotComponent.Setup(userItem, quantity);
            if (!hasSimulateClickOnFirst)
            {
                slotComponent.OnItemClick();
                hasSimulateClickOnFirst = true;
            }
        }
    }

    public void ShowItemDetails(string itemName, string description, int quantity, int maxQuantity, Sprite sprite)
    {
        itemNameText.text = itemName;
        itemDescriptionText.text = description;
        itemQuantityText.text = quantity + (maxQuantity > 0 ? " / " + maxQuantity : "");
        itemIconImage.sprite = sprite;
    }
}




