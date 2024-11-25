using UnityEngine;
using UnityEngine.UI;

public class InventoryFilterButton : MonoBehaviour
{

    [SerializeField] private Button button;
    public string filterCategory; // Définit le filtre de ce bouton (par exemple: "Weapons", "Meals", etc.)

    private void Awake()
    {
        button.onClick.AddListener(OnFilterButtonClick);
    }
    
    private void OnFilterButtonClick()
    {
        // Appeler la méthode SetFilter de l'inventaire principal
        FindObjectOfType<Inventory>().CurrentFilter = filterCategory;
    }
}
