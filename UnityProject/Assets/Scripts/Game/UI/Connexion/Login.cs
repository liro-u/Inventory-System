using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI message;
    public TMP_InputField idField;
    public TMP_InputField passwordField;

    public void LoginButton()
    {
        string identifier = idField.text;
        string password = passwordField.text;

        // Start the login process via UserService
        StartCoroutine(UserService.Login(identifier, password, OnLoginSuccess, OnLoginError));
    }

    // This will be called on successful login
    public void OnLoginSuccess(UserData userData)
    {
        Debug.Log("Login successful");
        GameDataManager.Instance.SetUserData(userData);
        message.text = "";
        ConnexionUIManager.Instance.ConnexionUI.SetActive(false);
    }

    // This will be called if the login fails
    public void OnLoginError(UserAPIErrorResponse userAPIErrorResponse)
    {
        Debug.Log(userAPIErrorResponse.errors);
        string errorMessage = userAPIErrorResponse.errors.global;
        Debug.LogError("Login failed: " + errorMessage);
        message.text = "Error: " + errorMessage;
    }
}
