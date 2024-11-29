using UnityEngine;
using UnityEngine.UI;

public class InventoryFilterButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public string filterCategory; // Définit le filtre de ce bouton (par exemple: "Weapons", "Meals", etc.)
    [SerializeField] private Image selectionFrame; // Image utilisée pour encadrer le bouton sélectionné

    [Header("Selection Frame Colors")]
    [SerializeField] private Color defaultColor = Color.white; // Couleur par défaut
    [SerializeField] private Color selectedColor = Color.yellow; // Couleur de sélection (Gold)

    private void Awake()
    {
        // Ajouter un listener pour gérer le clic sur le bouton
        button.onClick.AddListener(OnFilterButtonClick);

        // Initialiser tous les cadres avec la couleur par défaut
        if (selectionFrame != null)
        {
            selectionFrame.color = defaultColor;
            selectionFrame.enabled = true; // Le cadre est toujours visible
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

        // Réinitialiser les couleurs de tous les cadres
        foreach (var filterButton in allButtons)
        {
            if (filterButton.selectionFrame != null)
            {
                filterButton.selectionFrame.color = defaultColor;
            }
        }

        // Appliquer la couleur dorée pour le cadre de ce bouton
        if (selectionFrame != null)
        {
            selectionFrame.color = selectedColor;
        }
    }
}
