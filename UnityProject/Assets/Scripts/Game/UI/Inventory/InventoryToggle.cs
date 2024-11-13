using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private bool enableByDefault = false;
    [SerializeField] private KeyCode openInventoryKey = KeyCode.LeftShift;
    private void Awake()
    {
        inventoryUI.SetActive(enableByDefault);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(openInventoryKey))
        {
            OnOpenInventoryPerformed();
        }
    }

    public void OnOpenInventoryPerformed()
    {
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
    }



}
