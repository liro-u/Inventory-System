using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI message;
    public TMP_InputField idField;
    public TMP_InputField passwordField;
    public Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(LoginButton);
    }

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
        GameDataManager.Instance.ConnectionData.CurrentUserData = userData;
        message.text = "";
        UIManager.Instance.GetCurrentUIInHistory().UIRef.CloseAllPopup();
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
