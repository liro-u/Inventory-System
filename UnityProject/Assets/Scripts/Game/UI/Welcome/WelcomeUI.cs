using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI userDataText;
    public GameObject[] connectedUI;
    public GameObject[] notConnectedUI;
    public Button quitButton;
    public Button logoutButton;
    public Button loginButton;
    public Button settingsButton;
    public Button startGameButton;

    [Header("Debug")]
    public bool forceConnectionMode = false;

    void Start()
    {
        GameDataManager.Instance.OnUserConnection += OnUserConnexion;
        quitButton.onClick.AddListener(QuitGame);
        startGameButton.onClick.AddListener(StartGame);
    }

    private void Awake()
    {
        ShowConnectedUI(forceConnectionMode);
        ShowNotConnectedUI(!forceConnectionMode);
    }

    public void OnUserConnexion()
    {
        userDataText.text = GameDataManager.Instance.currentUserData.pseudo;
        ShowConnectedUI(true);
        ShowNotConnectedUI(false);
    }

    private void ShowConnectedUI(bool show)
    {
        for (int i = 0; i < connectedUI.Length; i++)
        {
            connectedUI[i].SetActive(show);
        }
    }

    private void ShowNotConnectedUI(bool show)
    {
        for (int i = 0; i < notConnectedUI.Length; i++)
        {
            notConnectedUI[i].SetActive(show);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // If in the Unity Editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If built, quit the application
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        gameObject.SetActive(false);
    }
}
