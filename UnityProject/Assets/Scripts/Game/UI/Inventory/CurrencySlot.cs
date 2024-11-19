using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencySlot : MonoBehaviour
{
    private Currency currency;
    private Sprite sprite;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;

    [Header("Setup")]
    [SerializeField] private string currencyId = "";

    private void Awake()
    {
        button.onClick.AddListener(OnCurrencyClick);
        Setup();
    }

    public void Setup()
    {
        UserCurrency userCurrency = GameDataManager.Instance.CurrencyData.FindUserCurrencyById(currencyId);

        sprite = GameDataManager.Instance.CurrencyAdditionalData.GetAdditionalDataById(currencyId).sprite;
        currency = null;
        string quantityString = "0 +";

        if (userCurrency != null)
        {
            currency = userCurrency.currencyId;
            quantityString = userCurrency.quantity.ToString() + " +";
        }

        iconImage.sprite = sprite;
        quantityText.text = quantityString;
    }

    public void OnCurrencyClick()
    {
        
    }
}
