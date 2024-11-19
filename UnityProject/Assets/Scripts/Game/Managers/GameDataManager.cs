using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] private ItemDictionarySO itemAdditionalData;
    [SerializeField] private CurrencyDictionarySO currencyAdditionalData;

    [Header("Live Game Data")]
    [SerializeField] private ConnectionDataSO connectionData;
    [SerializeField] private InventoryDataSO inventoryData;
    [SerializeField] private CurrencyDataSO currencyData;

    // Properties to expose the fields with a public getter but no public setter
    public ItemDictionarySO ItemAdditionalData => itemAdditionalData;
    public CurrencyDictionarySO CurrencyAdditionalData => currencyAdditionalData;

    public ConnectionDataSO ConnectionData => connectionData;
    public InventoryDataSO InventoryData => inventoryData;
    public CurrencyDataSO CurrencyData => currencyData;

    // Singleton pattern to easily access GameDataManager
    private static GameDataManager _instance;
    public static GameDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameDataManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameDataManager");
                    _instance = go.AddComponent<GameDataManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        connectionData = ScriptableObject.CreateInstance<ConnectionDataSO>();
        inventoryData = ScriptableObject.CreateInstance<InventoryDataSO>();
        currencyData = ScriptableObject.CreateInstance<CurrencyDataSO>();

        connectionData.OnUserConnection += () => inventoryData.FetchUserInventory(this);
        connectionData.OnUserConnection += () => currencyData.FetchUserCurrency(this);
    }
}
