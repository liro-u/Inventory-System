using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Inventory
{
    public class SortDirection : MonoBehaviour
    {
        public bool reversed = false;

        [SerializeField] private Image arrowImage; // Image qui représentera la direction
        [SerializeField] private Button button;
        [SerializeField] private global::Inventory inventory;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            reversed = !reversed;

            // Rotation de l'image
            float rotationAngle = reversed ? 180f : 0f;
            arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);

            // Mettre à jour l'inventaire
            inventory.UpdateInventoryGrid();
        }
    }
}
