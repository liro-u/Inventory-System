using System.Collections.Generic;
using UnityEngine;

public class InventoryDataSO : ScriptableObject
{
    [SerializeField] private List<UserItem> userInventory = new List<UserItem>();

    // Public getter with private setter for inventoryData
    public List<UserItem> UserInventory
    {
        get => userInventory;
        set
        {
            userInventory = value;
            OnUserInventoryChange.Invoke();
        }
    }

    // Delegate and event for user inventory
    public delegate void UserInventoryHandler();
    public event UserInventoryHandler OnUserInventoryChange = delegate { };

    public void FetchUserInventory(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(UserItemService.FetchUserItems(OnFetchUserItemsSuccess, OnFetchUserItemsError));
    }

    private void OnFetchUserItemsSuccess(UserItems userItems)
    {
        Debug.Log("Fetch Inventory Successfully");
        var userItemsList = new List<UserItem>(userItems.items);
        GameDataManager.Instance.InventoryData.UserInventory = userItemsList;
    }

    private void OnFetchUserItemsError(GameAPIErrorResponse error)
    {
        Debug.LogError(error.errors.global);
    }
}