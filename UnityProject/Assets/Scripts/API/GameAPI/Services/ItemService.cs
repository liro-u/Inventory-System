using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public static class ItemService
{
    private const string getAllItemsEndpoint = "/api/items";

    // This method sends a GET request to get all existing items
    public static IEnumerator GetAllItems(Action<Items> onSuccess, Action<GameAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.GameApiUrl + getAllItemsEndpoint;

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
            // Parse the response into Item
            Items items = JsonUtility.FromJson<Items>(request.downloadHandler.text);
            onSuccess?.Invoke(items);
        }
    }
}