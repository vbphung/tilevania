using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories.Equipments
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] EquipableItem[] startingItems;

        Dictionary<EquipLocation, EquipableItem> itemSlots;

        public event Action equipmentUpdated;

        private void Awake()
        {
            itemSlots = new Dictionary<EquipLocation, EquipableItem>();
            for (int i = 0; i < startingItems.Length; ++i)
                itemSlots[startingItems[i].GetEquipLocation()] = startingItems[i];
        }

        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            return itemSlots.ContainsKey(equipLocation) ? itemSlots[equipLocation] : null;
        }

        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            itemSlots[slot] = item;

            if (equipmentUpdated != null)
                equipmentUpdated();
        }

        public void RemoveItem(EquipLocation slot)
        {
            itemSlots.Remove(slot);

            if (equipmentUpdated != null)
                equipmentUpdated();
        }
    }
}