using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

public static class UserService
{
    private const string loginEndpoint = "/api/user/login";
    private const string signupEndpoint = "/api/user/signup";

    // This method sends a POST request to login the user
    public static IEnumerator Login(string identifier, string password, Action<UserData> onSuccess, Action<UserAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.UserApiUrl + loginEndpoint;
        WWWForm form = new WWWForm();
        form.AddField("identifier", identifier);
        form.AddField("password", password);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            // Parse the response into UserData
            UserAPIErrorResponse userAPIErrorResponse = JsonUtility.FromJson<UserAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(userAPIErrorResponse);
        }
        else
        {
            // Parse the response into UserData
            UserData userData = JsonUtility.FromJson<UserData>(request.downloadHandler.text);
            onSuccess?.Invoke(userData);
        }
    }

    // This method sends a POST request to register a new user
    public static IEnumerator Signup(string email, string password, Action<UserData> onSuccess, Action<UserAPIErrorResponse> onError)
    {
        string url = APIManager.Instance.UserApiUrl + signupEndpoint;
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            // Parse the response into UserData
            UserAPIErrorResponse userAPIErrorResponse = JsonUtility.FromJson<UserAPIErrorResponse>(request.downloadHandler.text);
            onError?.Invoke(userAPIErrorResponse);
        }
        else
        {
            // Parse the response into UserData
            UserData userData = JsonUtility.FromJson<UserData>(request.downloadHandler.text);
            onSuccess?.Invoke(userData);
        }
    }
}

