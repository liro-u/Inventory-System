using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.E;
    [SerializeField] private string UIName = "";

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            OnToggle();
        }
    }

    public void OnToggle()
    {
        UILinker currentUI = UIManager.Instance.GetCurrentUIInHistory();
        if (currentUI != null && currentUI.UIName == UIName)
        {
            UIManager.Instance.GoBackInHistory();
        }
        else
        {
            UIManager.Instance.OpenAndAddInHistoryByName(UIName);
        }
    }



}
