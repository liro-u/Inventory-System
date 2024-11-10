using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryGrid;
    public GameObject ItemSlotPrefab;

    void Start()
    {
        // Start the inventory fetch process via UserItemService
        StartCoroutine(UserItemService.FetchUserItems(OnFetchUserItemsSuccess, OnFetchUserItemsError));
    }

    void OnFetchUserItemsSuccess(UserItems userItems)
    {
        Debug.Log("Fetch Inventory Successfuly");
        List<UserItem> userItemsList = new List<UserItem>(userItems.items);
        GameDataManager.Instance.SetUserInventory(userItemsList);
        UpdateInventoryGrid();
    }

    void OnFetchUserItemsError(GameAPIErrorResponse error)
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
        List<UserItem> userInventory = GameDataManager.Instance.userInventory;

        // Loop through the inventory list and create a slot for each item
        for (int i = 0; i < userInventory.Count; i++)
        {
            UserItem item = userInventory[i];

            // Instantiate a new item slot
            GameObject newSlot = Instantiate(ItemSlotPrefab, inventoryGrid.transform);

            // Here you can set up the slot's properties based on the item data
            // Example: Set item name, quantity, or icon in the slot
            //ItemSlot slotComponent = newSlot.GetComponent<ItemSlot>();
            //if (slotComponent != null)
            //{
            //    slotComponent.Setup(item); // Assuming 'Setup' is a method in 'ItemSlot' to initialize the slot
            //}
        }
    }

}
