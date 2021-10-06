using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int inventorySize = 16;
        [SerializeField] InventorySlot[] startingItems;

        InventorySlot[] itemSlots;

        public event Action inventoryUpdated;

        [System.Serializable]
        public struct InventorySlot
        {
            public InventoryItem item;
            public int number;
        }

        private void Awake()
        {
            itemSlots = new InventorySlot[inventorySize];
            for (int i = 0; i < startingItems.Length; ++i)
                itemSlots[i] = startingItems[i];
        }

        public Dictionary<InventoryItem, int> GetAllItems()
        {
            Dictionary<InventoryItem, int> result = new Dictionary<InventoryItem, int>();
            for (int i = 0; i < itemSlots.Length; ++i)
                if (itemSlots[i].item != null)
                    result[itemSlots[i].item] = itemSlots[i].number;
            return result;
        }

        public int GetSize()
        {
            return inventorySize;
        }

        public int GetFreeSlots()
        {
            int result = 0;
            foreach (InventorySlot itemSlot in itemSlots)
                if (itemSlot.number == 0)
                    ++result;
            return result;
        }

        public bool HasSpaceForItem(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < itemSlots.Length; ++i)
                if (object.ReferenceEquals(itemSlots[i].item, item))
                    return true;
            return false;
        }

        public InventoryItem GetItemInSlot(int slot)
        {
            return itemSlots[slot].item;
        }

        public int GetNumberInSlot(int slot)
        {
            return itemSlots[slot].number;
        }

        public void RemoveFromSlot(int slot, int number)
        {
            itemSlots[slot].number -= number;

            if (itemSlots[slot].number <= 0)
            {
                itemSlots[slot].number = 0;
                itemSlots[slot].item = null;
            }

            if (inventoryUpdated != null)
                inventoryUpdated();
        }

        public void RemoveItems(InventoryItem item, int number)
        {
            int itemSlot = FindSlotContainItem(item);

            while (itemSlot >= 0)
            {
                int itemNumber = itemSlots[itemSlot].number;
                if (itemNumber > number)
                {
                    RemoveFromSlot(itemSlot, number);
                    return;
                }
                else
                {
                    RemoveFromSlot(itemSlot, itemNumber);
                    number -= itemNumber;
                    itemSlot = FindSlotContainItem(item);
                }
            }
        }

        public bool AddToFirstEmptySlot(InventoryItem item, int number)
        {
            if (number <= 0)
                return true;

            int i = FindSlot(item);
            if (i >= 0)
            {
                itemSlots[i].item = item;
                itemSlots[i].number += number;

                if (inventoryUpdated != null)
                    inventoryUpdated();

                return true;
            }

            return false;
        }

        public bool AddItemToSlot(int slot, InventoryItem item, int number)
        {
            if (itemSlots[slot].item != null)
                return AddToFirstEmptySlot(item, number); ;

            int stackIndex = FindStack(item);
            if (stackIndex >= 0)
                slot = stackIndex;

            itemSlots[slot].item = item;
            itemSlots[slot].number += number;

            if (inventoryUpdated != null)
                inventoryUpdated();

            return true;
        }

        private int FindSlot(InventoryItem item)
        {
            int stackIndex = FindStack(item);
            if (stackIndex < 0)
                return FindEmptySlot();
            return stackIndex;
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < itemSlots.Length; ++i)
                if (itemSlots[i].item == null)
                    return i;
            return -1;
        }

        private int FindStack(InventoryItem item)
        {
            if (!item.IsStackable())
                return -1;

            for (int i = 0; i < itemSlots.Length; ++i)
                if (object.ReferenceEquals(itemSlots[i].item, item))
                    return i;

            return -1;
        }

        private int FindSlotContainItem(InventoryItem item)
        {
            for (int i = 0; i < itemSlots.Length; ++i)
                if (object.ReferenceEquals(itemSlots[i].item, item))
                    return i;
            return -1;
        }
    }
}