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
        // Clear all existing children in the inventory grid
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

                    // Here you can set up the slot's properties based on the item data
                    var quantityTransform = newSlot.transform.Find("Item Quantity");
                    if (quantityTransform is not null)
                    {
                        quantityTransform.GetComponent<TextMeshPro>().text =
                            (quantity > userItem.itemId.maxQuantityPerSlot
                                ? userItem.itemId.maxQuantityPerSlot
                                : quantity) + "/" + userItem.itemId.maxQuantityPerSlot;
                    }

                    quantity -= userItem.itemId.maxQuantityPerSlot;
                }
            }
            else
            {
                // Instantiate a new item slot
                var newSlot = Instantiate(ItemSlotPrefab, inventoryGrid.transform);

                // Here you can set up the slot's properties based on the item data
                var quantityTransform = newSlot.transform.Find("Item Quantity");
                if (quantityTransform is not null)
                {
                    quantityTransform.GetComponent<TextMeshPro>().text =
                        quantity + "/" + userItem.itemId.maxQuantityPerSlot;
                }
            }
        }
    }
}