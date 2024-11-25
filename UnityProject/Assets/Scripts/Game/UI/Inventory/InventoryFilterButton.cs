using UnityEngine;
using UnityEngine.UI;

public class InventoryFilterButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public string filterCategory; // Définit le filtre de ce bouton (par exemple: "Weapons", "Meals", etc.)
    [SerializeField] private Image selectionFrame; // Image utilisée pour encadrer le bouton sélectionné

    private void Awake()
    {
        // Ajouter un listener pour gérer le clic sur le bouton
        button.onClick.AddListener(OnFilterButtonClick);

        // Assurez-vous que le cadre de sélection est désactivé par défaut
        if (selectionFrame != null)
        {
            selectionFrame.enabled = false;
        }
    }

    private void OnFilterButtonClick()
    {
        // Appeler la méthode SetFilter de l'inventaire principal
        FindObjectOfType<Inventory>().CurrentFilter = filterCategory;

        // Définir ce bouton comme sélectionné
        SetSelected();
    }

    public void SetSelected()
    {
        // Trouver tous les boutons de filtre
        var allButtons = FindObjectsOfType<InventoryFilterButton>();

        // Désactiver les cadres de sélection de tous les boutons
        foreach (var filterButton in allButtons)
        {
            if (filterButton.selectionFrame != null)
            {
                filterButton.selectionFrame.enabled = false;
            }
        }

        // Activer le cadre de sélection pour ce bouton
        if (selectionFrame != null)
        {
            selectionFrame.enabled = true;
        }
    }
}
