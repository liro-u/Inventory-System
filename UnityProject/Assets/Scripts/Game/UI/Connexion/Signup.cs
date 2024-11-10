using UnityEngine;
using TMPro;

public class Signup : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI message;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    /// <summary>
    /// Called when the signup button is pressed. Retrieves user input and starts the signup process.
    /// </summary>
    public void SignupButton()
    {
        string email = emailField.text;
        string password = passwordField.text;

        // Start the signup process via UserService
        StartCoroutine(UserService.Signup(email, password, OnSignupSuccess, OnSignupError));
    }

    /// <summary>
    /// This will be called on successful signup.
    /// </summary>
    public void OnSignupSuccess(UserData userData)
    {
        Debug.Log("Signup successful");
        GameDataManager.Instance.SetUserData(userData);
        message.text = "";
        ConnexionUIManager.Instance.ConnexionUI.SetActive(false);
    }

    /// <summary>
    /// This will be called if the signup fails.
    /// </summary>
    public void OnSignupError(UserAPIErrorResponse userAPIErrorResponse)
    {
        string errorMessage = userAPIErrorResponse.errors.global;
        Debug.LogError("Signup failed: " + errorMessage);
        message.text = "Error: " + errorMessage;
    }
}
