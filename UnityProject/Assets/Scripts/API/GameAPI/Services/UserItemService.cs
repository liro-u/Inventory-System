using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public static class UserItemService
{
    private const string addItemQuantityEndpoint = "/api/userItems/addItemQuantity";
    private const string removeItemQuantityEndpoint = "/api/userItems/removeItemQuantity";
    private const string getUserItemsEndpoint = "/api/userItems";

    // This method sends a PATCH request to add a quantity of item to the user
    public static IEnumerator AddItemQuantityToUser(string itemId, int quantity, Action<UserItem> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + addItemQuantityEndpoint;

        var payload = new { itemId, quantity };
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
            GameAPIErrorResponse gameAPIErrorResponse = JsonUtility.FromJson<GameAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(gameAPIErrorResponse);
        }
        else
        {
            UserItem userItem = JsonUtility.FromJson<UserItem>(request.downloadHandler.text);
            onSuccess?.Invoke(userItem);
        }
    }


    public static IEnumerator RemoveItemQuantityFromUser(string itemId, int quantity, Action<UserItem> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + removeItemQuantityEndpoint;

        var payload = new { itemId, quantity };
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
            // Parse the response into UserItem
            UserItem userItem = JsonUtility.FromJson<UserItem>(request.downloadHandler.text);
            onSuccess?.Invoke(userItem);
        }
    }

    public static IEnumerator FetchUserItems(Action<UserItems> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + getUserItemsEndpoint;

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
            // Parse the response into UserItem
            UserItems userItems = JsonUtility.FromJson<UserItems>(request.downloadHandler.text);
            onSuccess?.Invoke(userItems);
        }
    }
}
