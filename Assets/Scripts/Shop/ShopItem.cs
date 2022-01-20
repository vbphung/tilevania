using UnityEngine;

using Inventories;

namespace Shops
{
    public class ShopItem
    {
        InventoryItem item;
        int availability;
        float price;
        int quantityInTransaction;

        public ShopItem(InventoryItem item, int availability, float price, int quantityInTransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetAvailability()
        {
            return availability;
        }

        public float GetPrice()
        {
            return price;
        }

        public int GetQuantityInTransaction()
        {
            return quantityInTransaction;
        }
    }
}