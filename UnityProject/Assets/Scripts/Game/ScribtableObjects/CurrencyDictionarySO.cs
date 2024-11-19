using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a ScriptableObject to store your data
[CreateAssetMenu(fileName = "CurrencyDictionary", menuName = "ScriptableObjects/CurrencyDictionarySO", order = 1)]
public class CurrencyDictionarySO : ScriptableObject
{
    // List to store key-value pairs
    [SerializeField] private List<CurrencyDataPair> currencies = new List<CurrencyDataPair>();

    // Private dictionary for fast access
    private Dictionary<string, AdditionalCurrencyData> currencyDictionary = new Dictionary<string, AdditionalCurrencyData>();

    // Expose the list so you can access it from other scripts
    public List<CurrencyDataPair> Currencies => currencies;

    // Initialize the dictionary
    private void OnEnable()
    {
        currencyDictionary.Clear();
        foreach (var currency in currencies)
        {
            if (!currencyDictionary.ContainsKey(currency.key))
            {
                currencyDictionary.Add(currency.key, currency.value);
            }
        }
    }

    // Method to get AdditionalCurrencyData by _id
    public AdditionalCurrencyData GetAdditionalDataById(string _id)
    {
        currencyDictionary.TryGetValue(_id, out AdditionalCurrencyData additionalData);
        return additionalData;
    }
}
