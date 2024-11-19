using System.Collections.Generic;
using UnityEngine;

public class CurrencyDataSO : ScriptableObject
{
    [SerializeField] private List<UserCurrency> userCurrency = new List<UserCurrency>();

    // Public getter with private setter for currencyData
    public List<UserCurrency> UserCurrency
    {
        get => userCurrency;
        set
        {
            userCurrency = value;
            OnUserCurrencyChange.Invoke();
        }
    }

    // Delegate and event for user currency
    public delegate void UserCurrencyHandler();
    public event UserCurrencyHandler OnUserCurrencyChange = delegate { };

    public void FetchUserCurrency(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(UserCurrencyService.FetchUserCurrencies(OnFetchUserCurrenciesSuccess, OnFetchUserCurrenciesError));
    }

    private void OnFetchUserCurrenciesSuccess(UserCurrencies userCurrencies)
    {
        Debug.Log("Fetch Currency Successfully");
        var userCurrenciesList = new List<UserCurrency>(userCurrencies.currencies);
        GameDataManager.Instance.CurrencyData.UserCurrency = userCurrenciesList;
    }

    private void OnFetchUserCurrenciesError(GameAPIErrorResponse error)
    {
        Debug.LogError(error.errors.global);
    }

    // Method to find a UserCurrency by currencyId._id
    public UserCurrency FindUserCurrencyById(string currencyId)
    {
        foreach (var userCur in userCurrency)
        {
            if (userCur.currencyId != null && userCur.currencyId._id == currencyId)
            {
                return userCur;
            }
        }
        return null;
    }
}