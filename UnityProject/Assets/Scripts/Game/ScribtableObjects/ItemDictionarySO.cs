using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a ScriptableObject to store your data
[CreateAssetMenu(fileName = "ItemDictionary", menuName = "ScriptableObjects/ItemDictionarySO", order = 1)]
public class ItemDictionarySO : ScriptableObject
{
    // List to store key-value pairs
    [SerializeField] private List<ItemDataPair> items = new List<ItemDataPair>();

    // Private dictionary for fast access
    private Dictionary<string, AdditionalItemData> itemDictionary = new Dictionary<string, AdditionalItemData>();

    // Expose the list so you can access it from other scripts
    public List<ItemDataPair> Items => items;

    // Initialize the dictionary
    private void OnEnable()
    {
        itemDictionary.Clear();
        foreach (var item in items)
        {
            if (!itemDictionary.ContainsKey(item.key))
            {
                itemDictionary.Add(item.key, item.value);
            }
        }
    }

    // Method to get AdditionalItemData by _id
    public AdditionalItemData GetAdditionalDataById(string _id)
    {
        itemDictionary.TryGetValue(_id, out AdditionalItemData additionalData);
        return additionalData;
    }
}
