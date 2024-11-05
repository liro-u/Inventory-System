using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string _id;
    public string name; // trad
    public string description; // trad
    public int maxQuantity;
}

[System.Serializable]
public class UserItem
{
    public string _id;
    public int quantity;
    public Item item;
}
