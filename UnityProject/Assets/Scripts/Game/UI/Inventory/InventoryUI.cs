using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    void Awake()
    {
        Instance = this;
    }

    public void ShowItemDetails(string name, string description)
    {
        itemNameText.text = name;
        itemDescriptionText.text = description;
    }
}