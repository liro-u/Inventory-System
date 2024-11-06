using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Manages the login process, sending login data to the API and handling responses.
/// </summary>
public class Login : MonoBehaviour
{
    [Header("UI")]
    /// <summary>The message text UI to display message after fetch login.</summary>
    public TextMeshProUGUI message;
    /// <summary>The identifier input field to get data from when fetch login.</summary>
    public TMP_InputField idField;
    /// <summary>The password input field to get data from when fetch login.</summary>
    public TMP_InputField passwordField;

    /// <summary>
    /// Called when the login button is pressed. Retrieves user input and starts the login process.
    /// </summary>
    public void LoginButton()
    {
        // Get Data to send from UI
        string identifier = idField.text;
        string password = passwordField.text;

        // Create a LoginRequest object and populate it
        LoginRequest loginData = new LoginRequest
        {
            identifier = identifier,
            password = password
        };

        // Convert the LoginRequest object to JSON
        string jsonBody = JsonUtility.ToJson(loginData);

        StartCoroutine(FetchLogin("/api/user/login", jsonBody));
    }

    /// <summary>
    /// Sends a POST request with the login data to the specified API endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint for the login API.</param>
    /// <param name="jsonBody">The JSON-formatted body containing the login data.</param>
    /// <returns>IEnumerator for coroutine handling.</returns>
    private IEnumerator FetchLogin(string endpoint, string jsonBody)
    {
        // Set up the POST request
        UnityWebRequest request = new UnityWebRequest(APIManager.Instance.ApiUrl + endpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Set the request headers
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            OnLoginSuccess(request);
        }
        else
        {
            OnLoginError(request);
        }
    }

    /// <summary>
    /// Handles a successful login response, parses the response data, and stores it in PlayerPrefs.
    /// </summary>
    /// <param name="request">The UnityWebRequest containing the response data.</param>
    private void OnLoginSuccess(UnityWebRequest request)
    {
        Debug.Log("Response: " + request.downloadHandler.text);

        // Parse the JSON response
        LoginResponse responseJson = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

        // Store relevant data in PlayerPrefs
        PlayerPrefs.SetString("UserId", responseJson._id);
        PlayerPrefs.SetString("UserPseudo", responseJson.pseudo);
        PlayerPrefs.SetString("UserToken", responseJson.token);
        PlayerPrefs.SetString("UserLanguage", responseJson.preference.language);
        PlayerPrefs.SetInt("IsAdmin", responseJson.isAdmin ? 1 : 0);
        PlayerPrefs.SetInt("IsOwner", responseJson.isOwner ? 1 : 0);

        PlayerPrefs.Save();

        Debug.Log("User data saved in PlayerPrefs.");

        message.text = "";
    }

    /// <summary>
    /// Handles a failed login attempt, logs the error, and updates the UI message.
    /// </summary>
    /// <param name="request">The UnityWebRequest containing the error data.</param>
    private void OnLoginError(UnityWebRequest request)
    {
        Debug.LogError("Error: " + request.error);
        message.text = "Error when trying to login.";

        // Parse the JSON response
        LoginError loginError = JsonUtility.FromJson<LoginError>(request.downloadHandler.text);
        Debug.LogError("Error Message: " + loginError.errors.global);
        message.text = loginError.errors.global;
    }
}
