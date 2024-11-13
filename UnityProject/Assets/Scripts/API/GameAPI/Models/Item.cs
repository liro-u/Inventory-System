using UnityEngine;

[System.Serializable]
public class Item
{
    public string _id;
    public string name;
    public string description;
    public string type;
    public string rarity;
    public int maxQuantityPerSlot;
    public int maxSlot;
}

public class Items
{
    public Item[] items;
}

[System.Serializable]
public class AdditionalItemData
{
    public string name;
    public Sprite sprite;
    public GameObject prefab3D;
}

[System.Serializable]
public class ItemDataPair
{
    public string key;
    public AdditionalItemData value;
}
