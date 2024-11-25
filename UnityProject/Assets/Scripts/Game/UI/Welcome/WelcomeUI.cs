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
    public Button connectBackgroundButton;

    public enum SceneLoadMode
    {
        SceneId,
        SceneName,
        SerializedScene
    }

    [Header("Next Scene")]
    public SceneLoadMode sceneLoadMode = SceneLoadMode.SceneId;
    public int nextSceneId = 1;
    public string nextSceneName = "Game";

    // Serialized scene asset reference (used only in the Editor)
    public Object sceneAsset;

    [Header("Debug")]
    public bool forceConnectionMode = false;

    void Start()
    {
        GameDataManager.Instance.ConnectionData.OnUserConnection += OnUserConnexion;
        quitButton.onClick.AddListener(QuitGame);
        startGameButton.onClick.AddListener(StartGame);
        connectBackgroundButton.onClick.AddListener(() => UIManager.Instance.GetCurrentUIInHistory().UIRef.OpenPopupByName("Login"));
        loginButton.onClick.AddListener(() => UIManager.Instance.GetCurrentUIInHistory().UIRef.OpenPopupByName("Login"));
    }

    private void Awake()
    {
        ShowConnectedUI(forceConnectionMode);
        ShowNotConnectedUI(!forceConnectionMode);
    }

    public void OnUserConnexion()
    {
        userDataText.text = GameDataManager.Instance.ConnectionData.CurrentUserData.pseudo;
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
        switch (sceneLoadMode)
        {
            case SceneLoadMode.SerializedScene:
                if (sceneAsset != null)
                {
                    string scenePath = UnityEditor.AssetDatabase.GetAssetPath(sceneAsset);
                    string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Debug.LogError("Serialized Scene Asset is not set.");
                }
                break;

            case SceneLoadMode.SceneId:
                SceneManager.LoadScene(nextSceneId);
                break;

            case SceneLoadMode.SceneName:
                SceneManager.LoadScene(nextSceneName);
                break;
        }

        UIManager.Instance.CleanHistory();
    }
}
