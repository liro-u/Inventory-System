using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private Dictionary<GameObject, float> collectableItems = new Dictionary<GameObject, float>();

    private GameObject closestItem;
    private Material toonMaterial;

    [SerializeField] private float outlineWidth = 5f;
    [SerializeField] private Color32 outlineColor = new Color32(0xe9, 0xe9, 0xaf, 0xff);

    private void OnTriggerEnter(Collider collider)
    {
        GameObject aimTarget = collider.gameObject;
        if (aimTarget.CompareTag("Item"))
        {
            float distance = Vector3.Distance(transform.position, aimTarget.transform.position);
            if (!collectableItems.ContainsKey(aimTarget))
            {
                collectableItems.Add(aimTarget, distance);
                HighlightClosestItem();
            }
        }
    }

    private void Update()
    {
        UpdateDistances();
        HighlightClosestItem();
    }

    private void UpdateDistances()
    {
        foreach (var item in collectableItems.Keys.ToList())
        {
            collectableItems[item] = Vector3.Distance(transform.position, item.transform.position);
        }
    }

    private void HighlightClosestItem()
    {
        if (collectableItems.Count == 0) return;

        var closest = collectableItems.OrderBy(item => item.Value).First();
        if (closestItem != closest.Key)
        {
            RestoreMaterial();

            closestItem = closest.Key;
            toonMaterial = closestItem.transform.GetChild(0).GetComponent<Renderer>().material;
            toonMaterial.SetFloat("_Outline_Width", outlineWidth);
            toonMaterial.SetColor("_Outline_Color", outlineColor);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        GameObject item = collider.gameObject;
        if (collectableItems.ContainsKey(item))
        {
            collectableItems.Remove(item);
            if (item == closestItem)
            {
                RestoreMaterial();
                HighlightClosestItem();
            }
        }
    }

    private void RestoreMaterial()
    {
        if (closestItem != null)
        {
            toonMaterial.SetFloat("_Outline_Width", 0f);
            closestItem = null;
        }
    }

    private void OnPickUp()
    {
        if (collectableItems.Count > 0)
        {
            Debug.Log("Pick up: 1 " + closestItem.name);
            ItemData itemData = closestItem.GetComponent<ItemData>();

            if (itemData != null)
            {
                if (!itemData.waitingForAPI)
                {
                    //Debug.Log("try pick up item");
                    itemData.waitingForAPI = true;
                    StartCoroutine(UserItemService.AddItemQuantityToUser(itemData._id, 1, OnPickupSuccess, OnPickupError));
                }
                else
                {
                    //Debug.Log("item is waiting for api");
                }
            }
            else
            {
                Debug.LogError("no item data in the pickup object");
            }
        }
        else
        {
            //Debug.Log("Pick up: Nothing.");
        }
    }

    public void OnPickupSuccess(AddUserItem addUserItem)
    {
        Debug.Log("item picked up correctly");
        collectableItems.Remove(closestItem);

        Destroy(closestItem);
        closestItem = null;

        RestoreMaterial();
        HighlightClosestItem();

        GameDataManager.Instance.InventoryData.FetchUserInventory(this);
    }

    public void OnPickupError(GameAPIErrorResponse error)
    {
        Debug.LogError("error while adding item to inventory");
        Debug.LogError(error.errors.global);

        ItemData itemData = closestItem.GetComponent<ItemData>();

        if (itemData != null)
        {
            itemData.waitingForAPI = false;
        }
    }
}