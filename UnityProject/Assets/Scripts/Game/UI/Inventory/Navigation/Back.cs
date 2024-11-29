using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour
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
        // Appeler la fonction GoBack du UIManager
        var uiManager = UIManager.Instance; // Récupérer le singleton
        if (uiManager != null)
        {
            var result = uiManager.GoBack(); // Appeler la fonction GoBack

            if (result == null)
            {
                Debug.Log("No UI to go back to.");
            }
        }
        else
        {
            Debug.LogError("UIManager instance is not available.");
        }
    }
}

