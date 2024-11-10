using UnityEngine;

public class APIManager : MonoBehaviour
{
    public enum EnvironmentMode { Development, Production }

    [Header("Environment Settings")]
    public EnvironmentMode mode;

    // URLs for development and production environments for both APIs
    [Header("UserAPI")]
    public const string devUserApiUrl = "http://localhost:4000"; // User API in development
    public const string prodUserApiUrl = "https://userapi.productionurl.com"; // User API in production

    [Header("GameAPI")]
    public const string devGameApiUrl = "http://localhost:5000"; // Game API in development
    public const string prodGameApiUrl = "https://gameapi.productionurl.com"; // Game API in production

    /// <summary>
    /// Gets the base API URL for User API based on the current environment mode.
    /// </summary>
    public string UserApiUrl
    {
        get
        {
            return mode == EnvironmentMode.Development ? devUserApiUrl : prodUserApiUrl;
        }
    }

    /// <summary>
    /// Gets the base API URL for Game API based on the current environment mode.
    /// </summary>
    public string GameApiUrl
    {
        get
        {
            return mode == EnvironmentMode.Development ? devGameApiUrl : prodGameApiUrl;
        }
    }

    private static APIManager _instance;

    /// <summary>
    /// Provides a singleton instance of APIManager.
    /// </summary>
    public static APIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<APIManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("APIManager");
                    _instance = go.AddComponent<APIManager>();
                }
            }
            return _instance;
        }
    }
}
