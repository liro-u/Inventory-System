using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Manages the signup process, sending signup data to the API and handling responses.
/// </summary>
public class Signup : MonoBehaviour
{
    [Header("UI")]
    /// <summary>The message text UI to display message after fetch signup.</summary>
    public TextMeshProUGUI message;
    /// <summary>The identifier input field to get data from when fetch signup.</summary>
    public TMP_InputField emailField;
    /// <summary>The password input field to get data from when fetch signup.</summary>
    public TMP_InputField passwordField;

    /// <summary>
    /// Called when the signup button is pressed. Retrieves user input and starts the signup process.
    /// </summary>
    public void SignupButton()
    {
        // Get Data to send from UI
        string email = emailField.text;
        string password = passwordField.text;

        // Create a SignupRequest object and populate it
        SignupRequest signupData = new SignupRequest
        {
            email = email,
            password = password
        };

        // Convert the SignupRequest object to JSON
        string jsonBody = JsonUtility.ToJson(signupData);

        StartCoroutine(FetchSignup("/api/user/signup", jsonBody));
    }

    /// <summary>
    /// Sends a POST request with the signup data to the specified API endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint for the signup API.</param>
    /// <param name="jsonBody">The JSON-formatted body containing the signup data.</param>
    /// <returns>IEnumerator for coroutine handling.</returns>
    private IEnumerator FetchSignup(string endpoint, string jsonBody)
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
            OnSignupSuccess(request);
        }
        else
        {
            OnSignupError(request);
        }
    }

    /// <summary>
    /// Handles a successful signup response, parses the response data, and stores it in PlayerPrefs.
    /// </summary>
    /// <param name="request">The UnityWebRequest containing the response data.</param>
    private void OnSignupSuccess(UnityWebRequest request)
    {
        Debug.Log("Response: " + request.downloadHandler.text);

        // Parse the JSON response
        SignupResponse responseJson = JsonUtility.FromJson<SignupResponse>(request.downloadHandler.text);

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
    /// Handles a failed signup attempt, logs the error, and updates the UI message.
    /// </summary>
    /// <param name="request">The UnityWebRequest containing the error data.</param>
    private void OnSignupError(UnityWebRequest request)
    {
        Debug.LogError("Error: " + request.error);
        message.text = "Error when trying to signup.";

        // Parse the JSON response
        SignupError signupError = JsonUtility.FromJson<SignupError>(request.downloadHandler.text);
        Debug.LogError("Error Message: " + signupError.errors.global);
        message.text = signupError.errors.global;
    }
}
