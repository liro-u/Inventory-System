using UnityEngine;

[System.Serializable]
public class Currency
{
    public string _id;
    public string name;
    public string description;
    public int maxQuantity;
}

public class Currencies
{
    public Currency[] currencies;
}

[System.Serializable]
public class AdditionalCurrencyData
{
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class CurrencyDataPair
{
    public string key;
    public AdditionalCurrencyData value;
}
