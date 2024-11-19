using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCurrencyDictionary : MonoBehaviour
{
    [Header("Target SO")]
    [SerializeField] private CurrencyDictionarySO currencyDictionarySO;

    [Header("Override Params")]
    [SerializeField] private bool overrideName = true;
    [SerializeField] private bool overrideTexture = false;

    [Header("Actions")]
    [SerializeField] private bool generateCurrencies = false;

    private void OnValidate()
    {
        if (generateCurrencies)
        {
            Generate();
            generateCurrencies = false;
        }
    }

    public void Generate()
    {
        StartCoroutine(CurrencyService.GetAllCurrencies(OnSuccess, OnError));
    }

    private void OnSuccess(Currencies currencies)
    {
        foreach (Currency currency in currencies.currencies)
        {
            var existingCurrency = currencyDictionarySO.Currencies.Find(x => x.key == currency._id);

            if (existingCurrency == null)
            {
                AdditionalCurrencyData additionalCurrencyData = new AdditionalCurrencyData
                {
                    name = currency.name,
                    sprite = null,
                };

                currencyDictionarySO.Currencies.Add(new CurrencyDataPair { key = currency._id, value = additionalCurrencyData });
            }
            else
            {
                if (overrideName)
                {
                    existingCurrency.value.name = currency.name;
                }
                if (overrideTexture)
                {
                    existingCurrency.value.sprite = null;
                }
            }
        }
    }

    private void OnError(GameAPIErrorResponse error)
    {
        Debug.LogError(error.errors.global);
    }
}
