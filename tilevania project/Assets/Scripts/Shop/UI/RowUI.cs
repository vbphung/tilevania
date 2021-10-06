using UnityEngine;
using UnityEngine.UI;

using Inventories;

namespace Shops.UI
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] Text nameText;
        [SerializeField] Text priceText;
        [SerializeField] Text availabilityText;
        [SerializeField] Text quantityText;

        Shop shop;
        InventoryItem item;

        public void Setup(Shop shop, ShopItem shopItem)
        {
            this.shop = shop;
            item = shopItem.GetItem();

            icon.sprite = item.GetIcon();
            nameText.text = item.name;
            priceText.text = shopItem.GetPrice().ToString() + "$";
            availabilityText.text = shopItem.GetAvailability().ToString();
            quantityText.text = shopItem.GetQuantityInTransaction().ToString();
        }

        public void AddToTransaction()
        {
            shop.AddToTransaction(item, 1);
        }

        public void RemoveToTransaction()
        {
            shop.AddToTransaction(item, -1);
        }
    }
}