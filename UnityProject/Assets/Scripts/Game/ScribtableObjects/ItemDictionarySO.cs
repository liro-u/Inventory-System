using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a ScriptableObject to store your data
[CreateAssetMenu(fileName = "ItemDictionary", menuName = "ScriptableObjects/ItemDictionarySO", order = 1)]
public class ItemDictionarySO : ScriptableObject
{
    // List to store key-value pairs
    [SerializeField] private List<ItemDataPair> items = new List<ItemDataPair>();

    // Expose the list so you can access it from other scripts
    public List<ItemDataPair> Items => items;
}
