using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public static class UserCurrencyService
{
    private const string addCurrencyQuantityEndpoint = "/api/userCurrencies/addCurrencyQuantity";
    private const string removeCurrencyQuantityEndpoint = "/api/userCurrencies/removeCurrencyQuantity";
    private const string getUserCurrenciesEndpoint = "/api/userCurrencies";

    // This method sends a PATCH request to add a quantity of currency to the user
    public static IEnumerator AddCurrencyQuantityToUser(string currencyId, int quantity, Action<AddUserCurrency> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        var url = APIManager.Instance.GameApiUrl + addCurrencyQuantityEndpoint;

        var payload = new { currencyId, quantity };
        var jsonBody = JsonUtility.ToJson(payload);

        var request = new UnityWebRequest(url, "PATCH");
        var bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        RequestUtils.AddAuthorizationHeader(request);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            var gameAPIErrorResponse = JsonUtility.FromJson<GameAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(gameAPIErrorResponse);
        }
        else
        {
            var userCurrency = JsonUtility.FromJson<AddUserCurrency>(request.downloadHandler.text);
            onSuccess?.Invoke(userCurrency);
        }
    }


    public static IEnumerator RemoveCurrencyQuantityFromUser(string currencyId, int quantity, Action<UserCurrency> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + removeCurrencyQuantityEndpoint;

        var payload = new { currencyId, quantity };
        string jsonBody = JsonUtility.ToJson(payload);

        UnityWebRequest request = new UnityWebRequest(url, "PATCH");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        RequestUtils.AddAuthorizationHeader(request);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            // Parse the response into GameAPIErrorResponse
            GameAPIErrorResponse gameAPIErrorResponse = JsonUtility.FromJson<GameAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(gameAPIErrorResponse);
        }
        else
        {
            // Parse the response into UserCurrency
            UserCurrency userCurrency = JsonUtility.FromJson<UserCurrency>(request.downloadHandler.text);
            onSuccess?.Invoke(userCurrency);
        }
    }

    public static IEnumerator FetchUserCurrencies(Action<UserCurrencies> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + getUserCurrenciesEndpoint;

        UnityWebRequest request = UnityWebRequest.Get(url);
        RequestUtils.AddAuthorizationHeader(request);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            // Parse the response into GameAPIErrorResponse
            GameAPIErrorResponse gameAPIErrorResponse = JsonUtility.FromJson<GameAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(gameAPIErrorResponse);
        }
        else
        {
            // Parse the response into UserCurrency
            UserCurrencies userCurrencies = JsonUtility.FromJson<UserCurrencies>(request.downloadHandler.text);
            onSuccess?.Invoke(userCurrencies);
        }
    }
}
