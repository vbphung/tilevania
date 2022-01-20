using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Inventories;

namespace Shops
{
    public class Shopper : MonoBehaviour
    {
        Inventory inventory;
        Purse purse;
        Shop activeShop;

        public event Action onActiveShopChanged;

        private void Awake()
        {
            inventory = GetComponent<Inventory>();
            purse = GetComponent<Purse>();
        }

        public void SetActiveShop(Shop shop)
        {
            activeShop = shop;

            if (onActiveShopChanged != null)
                onActiveShopChanged();
        }

        public Inventory GetInventory()
        {
            return inventory;
        }

        public Dictionary<InventoryItem, int> GetAllInventoryItems()
        {
            return inventory.GetAllItems();
        }

        public Purse GetPurse()
        {
            return purse;
        }

        public Shop GetActiveShop()
        {
            return activeShop;
        }

        public bool HasSufficientFund(float totalPrice)
        {
            return purse != null && purse.GetBalance() >= totalPrice;
        }

        public bool HasSufficientSpace(Dictionary<InventoryItem, int> transaction)
        {
            int needSlots = 0;
            foreach (KeyValuePair<InventoryItem, int> itemInfo in transaction)
                if (itemInfo.Key.IsStackable())
                    ++needSlots;
                else
                    needSlots += itemInfo.Value;

            return inventory.GetFreeSlots() >= needSlots;
        }

        public void TransactItem(InventoryItem item, int quantity, float price, bool buying)
        {
            if (buying)
            {
                if (item.IsStackable())
                    inventory.AddToFirstEmptySlot(item, quantity);
                else
                    for (int i = 0; i < quantity; ++i)
                        inventory.AddToFirstEmptySlot(item, 1);

                purse.UpdateBalance(-quantity * price);
            }
            else
            {
                inventory.RemoveItems(item, quantity);
                purse.UpdateBalance(quantity * price);
            }
        }
    }
}