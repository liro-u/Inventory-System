using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private string itemId;
    private string itemName;
    private string itemDescription;

    private void Awake()
    {
        // Attache l'événement de clic du bouton
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnItemClick);
        }
    }

    public void Setup(UserItem userItem)
    {
        itemId = userItem.itemId._id;
        itemName = userItem.itemId.name;
        itemDescription = userItem.itemId.description;

    }

    public void OnItemClick()
    {
        InventoryUI.Instance.ShowItemDetails(itemName, itemDescription);
    }
}
