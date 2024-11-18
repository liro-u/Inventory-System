using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryGrid;
    public GameObject ItemSlotPrefab;

    private void Start()
    {
        // Start the inventory fetch process via UserItemService
        StartCoroutine(UserItemService.FetchUserItems(OnFetchUserItemsSuccess, OnFetchUserItemsError));
    }

    private void OnFetchUserItemsSuccess(UserItems userItems)
    {
        Debug.Log("Fetch Inventory Successfully");
        var userItemsList = new List<UserItem>(userItems.items);
        GameDataManager.Instance.SetUserInventory(userItemsList);
        UpdateInventoryGrid();
    }

    private void OnFetchUserItemsError(GameAPIErrorResponse error)
    {
        Debug.LogError(error.errors.global);
    }

    private void UpdateInventoryGrid()
    {
        // Effacer tous les éléments enfants existants dans la grille d'inventaire
        foreach (Transform child in inventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // Get the user inventory from GameDataManager
        var userInventory = GameDataManager.Instance.userInventory;

        // Loop through the inventory list and create a slot for each item
        foreach (var userItem in userInventory)
        {
            var quantity = userItem.quantity;
            var numberOfSlots = Math.Ceiling((double)quantity / userItem.itemId.maxQuantityPerSlot);
            if (numberOfSlots > 0)
            {
                for (var i = 0; i < numberOfSlots; i++)
                {
                    // Instantiate a new item slot
                    var newSlot = Instantiate(ItemSlotPrefab, inventoryGrid.transform);

                    int currentQuantity = quantity > userItem.itemId.maxQuantityPerSlot ? userItem.itemId.maxQuantityPerSlot : quantity;

                    ItemSlot slotComponent = newSlot.GetComponent<ItemSlot>();
                    if (slotComponent != null)
                    {
                        slotComponent.Setup(userItem, quantity);
                    }

                    quantity -= userItem.itemId.maxQuantityPerSlot;
                }
            }
            else
            {
                // Instantiate a new item slot
                var newSlot = Instantiate(ItemSlotPrefab, inventoryGrid.transform);

                ItemSlot slotComponent = newSlot.GetComponent<ItemSlot>();
                if (slotComponent != null)
                {
                    slotComponent.Setup(userItem, quantity);
                }
            }
        }

    }
}




