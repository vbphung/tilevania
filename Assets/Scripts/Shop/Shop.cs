using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Inventories;
using Characters;

namespace Shops
{
    public class Shop : InteractableNPC
    {
        [SerializeField] string shopName;
        [Range(0, 100)] [SerializeField] float sellPercentage = 80f;
        [SerializeField] StockItem[] stockItems;

        [System.Serializable]
        public struct StockItem
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)] public float discountPercentage;
        }

        private float GetPrice(StockItem stockItem)
        {
            return stockItem.item.GetPrice() * (1 - stockItem.discountPercentage / 100);
        }

        private float GetPrice(InventoryItem item)
        {
            return item.GetPrice() * sellPercentage / 100;
        }

        private int GetAvailability(InventoryItem item)
        {
            if (buyingMode)
                return stock[item];
            return shopperInventory[item];
        }

        Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        bool buyingMode = true;
        ItemCategory filter;
        Shopper shopper;
        Dictionary<InventoryItem, int> shopperInventory;

        public event Action onShopUpdated;

        private void Awake()
        {
            foreach (StockItem stockItem in stockItems)
                stock[stockItem.item] = stockItem.initialStock;

            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
        }

        public string GetName()
        {
            return shopName;
        }

        public void SwitchMode()
        {
            buyingMode = !buyingMode;

            if (onShopUpdated != null)
                onShopUpdated();
        }

        public bool IsBuyingMode()
        {
            return buyingMode;
        }

        public void SetFilter(ItemCategory newFilter)
        {
            filter = newFilter;

            if (onShopUpdated != null)
                onShopUpdated();
        }

        public ItemCategory GetFilter()
        {
            return filter;
        }

        public IEnumerable<ShopItem> GetFilterItems()
        {
            if (filter == ItemCategory.None)
            {
                foreach (ShopItem shopItem in GetAllItems())
                    yield return shopItem;
                yield break;
            }

            foreach (ShopItem shopItem in GetAllItems())
                if (shopItem.GetItem().GetCategory() == filter)
                    yield return shopItem;
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            if (buyingMode)
            {
                foreach (StockItem stockItem in stockItems)
                {
                    int quantityInTransaction;
                    transaction.TryGetValue(stockItem.item, out quantityInTransaction);
                    yield return new ShopItem(stockItem.item, GetAvailability(stockItem.item), GetPrice(stockItem), quantityInTransaction);
                }

                yield break;
            }

            shopperInventory = shopper.GetAllInventoryItems();
            foreach (KeyValuePair<InventoryItem, int> itemInfo in shopperInventory)
            {
                int quantityInTransaction;
                transaction.TryGetValue(itemInfo.Key, out quantityInTransaction);
                yield return new ShopItem(itemInfo.Key, itemInfo.Value, GetPrice(itemInfo.Key), quantityInTransaction);
            }
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {
            if (!transaction.ContainsKey(item))
                transaction[item] = 0;

            int availability = GetAvailability(item);
            if (transaction[item] + quantity > availability)
                transaction[item] = availability;
            else
                transaction[item] += quantity;

            if (transaction[item] <= 0)
                transaction.Remove(item);

            if (onShopUpdated != null)
                onShopUpdated();
        }

        public void ConfirmTransaction()
        {
            if (shopper.GetInventory() != null && shopper.GetPurse() != null)
                foreach (ShopItem shopItem in GetAllItems())
                {
                    InventoryItem item = shopItem.GetItem();
                    int quantityInTransaction = shopItem.GetQuantityInTransaction();

                    transaction.Remove(item);
                    if (buyingMode)
                        stock[item] -= quantityInTransaction;

                    shopper.TransactItem(item, quantityInTransaction, shopItem.GetPrice(), buyingMode);
                }

            if (onShopUpdated != null)
                onShopUpdated();
        }

        public float TransactTotal()
        {
            float result = 0;
            foreach (ShopItem shopItem in GetAllItems())
                result += shopItem.GetPrice() * shopItem.GetQuantityInTransaction();

            return result;
        }

        public bool CanTransact()
        {
            if (transaction.Count == 0)
                return false;

            return shopper.HasSufficientFund(TransactTotal()) && shopper.HasSufficientSpace(transaction);
        }

        public override void OnMouseDown()
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                player.GetComponent<Shopper>().SetActiveShop(this);
        }
    }
}