using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories.Actions
{
    public class ActionStore : MonoBehaviour
    {
        [SerializeField] DockedItemSlot[] startingItems;

        Dictionary<int, DockedItemSlot> dockedItems;

        public event Action actionStoreUpdated;

        [System.Serializable]
        public class DockedItemSlot
        {
            public ActionItem item;
            public int number;
        }

        private void Awake()
        {
            dockedItems = new Dictionary<int, DockedItemSlot>();
            for (int i = 0; i < startingItems.Length; ++i)
                dockedItems[i] = startingItems[i];
        }

        public ActionItem GetActionInSlot(int index)
        {
            if (dockedItems.ContainsKey(index))
                return dockedItems[index].item;
            return null;
        }

        public int GetNumberInSlot(int index)
        {
            if (dockedItems.ContainsKey(index))
                return dockedItems[index].number;
            return 0;
        }

        public void AddItems(InventoryItem item, int index, int number)
        {
            if (dockedItems.ContainsKey(index))
            {
                if (object.ReferenceEquals(item, dockedItems[index].item))
                    dockedItems[index].number += number;
            }
            else
            {
                var slot = new DockedItemSlot();
                slot.item = item as ActionItem;
                slot.number = number;
                dockedItems[index] = slot;
            }

            if (actionStoreUpdated != null)
                actionStoreUpdated();
        }

        public void RemoveItems(int index, int number)
        {
            if (dockedItems.ContainsKey(index))
            {
                dockedItems[index].number -= number;
                if (dockedItems[index].number <= 0)
                    dockedItems.Remove(index);

                if (actionStoreUpdated != null)
                    actionStoreUpdated();
            }
        }
    }
}