using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] public string _id = "";
    [SerializeField] private float outlineMultiplier = 1f;

    public bool waitingForAPI = false;

    public void SetMaterialOutline(float outlineWidth, Color32 outlineColor)
    {
            Debug.Log("here wtf first");
        foreach (Renderer renderer in renderers)
        {
            Material toonMaterial = renderer.material;
            toonMaterial.SetFloat("_Outline_Width", outlineWidth * outlineMultiplier);
            toonMaterial.SetColor("_Outline_Color", outlineColor);
            Debug.Log("here wtf");
        }
    }
}
