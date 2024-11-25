using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Inventory
{
    public class SortScript : MonoBehaviour
    {
        public enum SortType
        {
            Default,
            Quantity,
            Rarity,
            Name,
            Obtained,
            Price,
        }

        public SortType sortType;
        public TextMeshProUGUI text;
        public SortDirection sortDirection;
        [SerializeField] private Button button;
        [SerializeField] private global::Inventory inventory;

        private void Start()
        {
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            sortType = sortType switch
            {
                SortType.Default => SortType.Quantity,
                SortType.Quantity => SortType.Rarity,
                SortType.Rarity => SortType.Name,
                SortType.Name => SortType.Obtained,
                SortType.Obtained => SortType.Price,
                SortType.Price => SortType.Default,
                _ => sortType
            };
            text.text = $"Sort by {sortType}";
            inventory.UpdateInventoryGrid();
        }

        public List<UserItem> SortUserItems(List<UserItem> userItems)
        {
            switch (sortType)
            {
                case SortType.Quantity:
                    userItems.Sort((item1, item2) => item1.quantity.CompareTo(item2.quantity));
                    break;
                case SortType.Rarity:
                    userItems.Sort((item1, item2) =>
                    {
                        if (item1.itemId.rarity == item2.itemId.rarity)
                        {
                            return item1.quantity.CompareTo(item2.quantity);
                        }

                        if (item1.itemId.rarity == "common" ||
                            item1.itemId.rarity == "rare" && item2.itemId.rarity != "common" ||
                            item1.itemId.rarity == "epic" && item2.itemId.rarity == "legendary")
                        {
                            return -1;
                        }

                        return 1;
                    });
                    break;
                case SortType.Name:
                    userItems.Sort((item1, item2) => string.Compare(item1.itemId.name, item2.itemId.name, StringComparison.Ordinal));
                    break;
                case SortType.Obtained:
                    // TODO
                case SortType.Price:
                    // TODO
                case SortType.Default:
                default:
                    break;
            }

            if (sortDirection.reversed)
            {
                userItems.Reverse();
            }

            return userItems;
        }
    }
}