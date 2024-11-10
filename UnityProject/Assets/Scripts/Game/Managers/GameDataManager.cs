using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Singleton pattern to easily access GameDataManager
    private static GameDataManager _instance;
    public static GameDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameDataManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameDataManager");
                    _instance = go.AddComponent<GameDataManager>();
                }
            }
            return _instance;
        }
    }

    [Header("Game Data")]
    public UserData currentUserData; // Holds the current user data in memory
    public List<UserItem> userInventory; // The user's inventory

    // Delegate and event for user connection
    public delegate void UserConnectionHandler();
    public event UserConnectionHandler OnUserConnection;

    // Set user data
    public void SetUserData(UserData userData)
    {
        currentUserData = userData;
        OnUserConnection?.Invoke();
    }

    // Set user inventory
    public void SetUserInventory(List<UserItem> userItems)
    {
        userInventory = userItems;
    }

    // Add an item to inventory
    public void AddItemToInventory(UserItem item)
    {
        userInventory.Add(item);
    }

    // Remove an item from inventory
    public void RemoveItemFromInventory(UserItem item)
    {
        userInventory.Remove(item);
    }

    // Get user inventory as a list
    public List<UserItem> GetInventory()
    {
        return userInventory;
    }
}
