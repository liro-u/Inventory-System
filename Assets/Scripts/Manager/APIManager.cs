using UnityEngine;

public class APIManager : MonoBehaviour
{
    public enum EnvironmentMode { Development, Production }

    [Header("Environment Settings")]
    public EnvironmentMode mode;

    // URLs for development and production environments
    private const string devUrl = "https://localhost:4000";
    private const string prodUrl = "https://yourproductionurl.com";

    /// <summary>
    /// Gets the base API URL based on the current environment mode.
    /// </summary>
    public string ApiUrl
    {
        get
        {
            return mode == EnvironmentMode.Development ? devUrl : prodUrl;
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

    /// <summary>
    /// Preserve instance between scene change
    /// </summary>
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Optionally, add initialization here to set mode based on build settings
}
