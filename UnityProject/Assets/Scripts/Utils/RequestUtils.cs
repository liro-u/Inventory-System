using UnityEngine.Networking;

public static class RequestUtils
{
    public static void AddAuthorizationHeader(UnityWebRequest request)
    {
        string token = GameDataManager.Instance.currentUserData.token;
        if (!string.IsNullOrEmpty(token))
        {
            request.SetRequestHeader("Authorization", "Bearer " + token);
        }
    }
}