using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItemDictionary : MonoBehaviour
{
    [Header("Target SO")]
    [SerializeField] private ItemDictionarySO itemDictionarySO;

    [Header("Override Params")]
    [SerializeField] private bool overrideName = true;
    [SerializeField] private bool overrideTexture = false;
    [SerializeField] private bool overridePrefab3D = false;

    [Header("Actions")]
    [SerializeField] private bool generateItems = false;

    private void OnValidate()
    {
        if (generateItems)
        {
            Generate();
            generateItems = false;
        }
    }

    public void Generate()
    {
        StartCoroutine(ItemService.GetAllItems(OnSuccess, OnError));
    }

    private void OnSuccess(Items items)
    {
        foreach (Item item in items.items)
        {
            var existingItem = itemDictionarySO.Items.Find(x => x.key == item._id);

            if (existingItem == null)
            {
                AdditionalItemData additionalItemData = new AdditionalItemData
                {
                    name = item.name,
                    sprite = null,
                    prefab3D = null
                };

                itemDictionarySO.Items.Add(new ItemDataPair { key = item._id, value = additionalItemData });
            }
            else
            {
                if (overrideName)
                {
                    existingItem.value.name = item.name;
                }
                if (overrideTexture)
                {
                    existingItem.value.sprite = null;
                }
                if (overridePrefab3D)
                {
                    existingItem.value.prefab3D = null;
                }
            }
        }
    }

    private void OnError(GameAPIErrorResponse error)
    {
        Debug.LogError(error.errors.global);
    }
}
