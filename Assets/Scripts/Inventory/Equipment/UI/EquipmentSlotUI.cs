using UnityEngine;

using Inventories.UI;
using UI.Dragging;

namespace Inventories.Equipments.UI
{
    public class EquipmentSlotUI : MonoBehaviour, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

        Equipment equipment;

        private void Awake()
        {
            equipment = GameObject.FindWithTag("Player").GetComponent<Equipment>();
            equipment.equipmentUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            icon.Setup(equipment.GetItemInSlot(equipLocation), 1);
        }

        public InventoryItem GetItem()
        {
            return equipment.GetItemInSlot(equipLocation) as InventoryItem;
        }

        public bool Acceptable(InventoryItem item)
        {
            EquipableItem equipableItem = item as EquipableItem;
            return equipableItem != null && equipment.GetItemInSlot(equipLocation) == null;
        }

        public void AddItems(InventoryItem item, int number)
        {
            equipment.AddItem(equipLocation, (EquipableItem)item);
        }

        public int GetNumber()
        {
            return equipment.GetItemInSlot(equipLocation) != null ? 1 : 0;
        }

        public void RemoveItems(int number)
        {
            equipment.RemoveItem(equipLocation);
        }
    }
}