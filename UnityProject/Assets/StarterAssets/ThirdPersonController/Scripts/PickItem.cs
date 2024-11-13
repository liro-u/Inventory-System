using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private Dictionary<GameObject, float> collectableItems = new Dictionary<GameObject, float>();

    private GameObject closestItem;
    private Material toonMaterial;

    private float outlineWidth = 5f;

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
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        GameObject item = collider.gameObject;
        if (collectableItems.ContainsKey(item)) {
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
            collectableItems.Remove(closestItem);
            //string itemId = GenerateItemDictionnary.idOf(itemSelected);
            //UserItemService.AddItemQuantityToUser(itemId, 1);
            Destroy(closestItem);
            closestItem = null;

            RestoreMaterial();
            HighlightClosestItem();
        } else
        {
            Debug.Log("Pick up: Nothing.");
        }
    }
}