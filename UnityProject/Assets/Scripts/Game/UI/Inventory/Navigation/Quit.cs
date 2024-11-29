using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnItemClick);
        }
        else
        {
            Debug.LogError("Button is not assigned in the Quit script.");
        }
    }

    private void OnItemClick()
    {
        // Appeler la fonction CleanHistory du UIManager
        var uiManager = UIManager.Instance; // Récupérer le singleton
        if (uiManager != null)
        {
            uiManager.CleanHistory(); // Appeler CleanHistory
            Debug.Log("UI history has been cleared.");
        }
        else
        {
            Debug.LogError("UIManager instance is not available.");
        }
    }
}