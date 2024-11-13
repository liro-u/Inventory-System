using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemQuantity;

    void Awake()
    {
        Instance = this;
    }

    public void ShowItemDetails(string itemName, string description, int quantity, int maxQuantity)
    {
        itemNameText.text = itemName;
        itemDescriptionText.text = description;
        itemQuantity.text = quantity + (maxQuantity > 0 ? " / " + maxQuantity : "");
    }
}