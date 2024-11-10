using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnexionUIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject ConnexionUI;
    public GameObject WelcomeUI;

    private static ConnexionUIManager _instance;

    /// <summary>
    /// Provides a singleton instance of APIManager.
    /// </summary>
    public static ConnexionUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ConnexionUIManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("UIManager");
                    _instance = go.AddComponent<ConnexionUIManager>();
                }
            }
            return _instance;
        }
    }

    public void ToggleConnexionUI()
    {
        ConnexionUI.SetActive(!ConnexionUI.activeInHierarchy);
    } 
}
