using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Inventory
{
    public class SortDirection : MonoBehaviour
    {
        public bool reversed = false;
    
        public TextMeshProUGUI text;
    
        [SerializeField] private Button button;
        [SerializeField] private global::Inventory inventory;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            reversed = !reversed;
            text.text = reversed ? "↑" : "↓";
            inventory.UpdateInventoryGrid();
        }
    }
}
