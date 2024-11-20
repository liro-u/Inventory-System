using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdditionalRarityData
{
    public Color color;
}

[System.Serializable]
public class RarityDataPair
{
    public string key;
    public AdditionalRarityData value;
}

// Create a ScriptableObject to store your data
[CreateAssetMenu(fileName = "RarityDictionary", menuName = "ScriptableObjects/RarityDictionarySO", order = 1)]
public class RarityDictionarySO : ScriptableObject
{
    // List to store key-value pairs
    [SerializeField] private List<RarityDataPair> rarities = new List<RarityDataPair>();

    // Private dictionary for fast access
    private Dictionary<string, AdditionalRarityData> rarityDictionary = new Dictionary<string, AdditionalRarityData>();

    // Expose the list so you can access it from other scripts
    public List<RarityDataPair> Rarities => rarities;

    // Initialize the dictionary
    private void OnEnable()
    {
        rarityDictionary.Clear();
        foreach (var rarity in rarities)
        {
            if (!rarityDictionary.ContainsKey(rarity.key))
            {
                rarityDictionary.Add(rarity.key, rarity.value);
            }
        }
    }

    // Method to get AdditionalRarityData by rarity
    public AdditionalRarityData GetAdditionalDataByRarity(string rarity)
    {
        rarityDictionary.TryGetValue(rarity, out AdditionalRarityData additionalData);
        return additionalData;
    }
}
