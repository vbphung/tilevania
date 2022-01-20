using UnityEngine;

using UI.Dragging;

namespace Inventories.UI
{
    public class InventorySlotUI : MonoBehaviour, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryIcon icon;

        Inventory inventory;
        int index;

        public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;

            icon.Setup(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
        }

        public InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public bool Acceptable(InventoryItem item)
        {
            InventoryItem inventoryItem = inventory.GetItemInSlot(index);
            if (inventoryItem == null)
                return true;

            return inventoryItem.IsStackable() && object.ReferenceEquals(inventoryItem, item);
        }

        public void AddItems(InventoryItem item, int number)
        {
            inventory.AddItemToSlot(index, item, number);
        }

        public int GetNumber()
        {
            return inventory.GetNumberInSlot(index);
        }

        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index, number);
        }
    }
}