using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public static class CurrencyService
{
    private const string getAllCurrenciesEndpoint = "/api/currencies";

    // This method sends a GET request to get all existing currencies
    public static IEnumerator GetAllCurrencies(Action<Currencies> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + getAllCurrenciesEndpoint;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            // Parse the response into GameAPIErrorResponse
            GameAPIErrorResponse userAPIErrorResponse = JsonUtility.FromJson<GameAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(userAPIErrorResponse);
        }
        else
        {
            // Parse the response into Currencies
            Currencies currencies = JsonUtility.FromJson<Currencies>(request.downloadHandler.text);
            onSuccess?.Invoke(currencies);
        }
    }
}