using UnityEngine;
using UnityEngine.UI;

using Inventories;

namespace Shops.UI
{
    public class FilterButton : MonoBehaviour
    {
        [SerializeField] ItemCategory category = ItemCategory.None;

        Button button;
        Shop shop;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(SetFilterToShop);
        }

        public void SetShop(Shop newShop)
        {
            shop = newShop;
        }

        public void Redraw()
        {
            button.interactable = shop.GetFilter() != category;
        }

        private void SetFilterToShop()
        {
            shop.SetFilter(category);
        }
    }
}