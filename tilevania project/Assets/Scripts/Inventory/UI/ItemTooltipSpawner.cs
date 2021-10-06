using UnityEngine;

using UI;
using Inventories.Actions.UI;
using Inventories.Equipments.UI;

namespace Inventories.UI
{
    public class ItemTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            InventorySlotUI inventorySlot = GetComponent<InventorySlotUI>();
            if (inventorySlot != null)
                return inventorySlot.GetItem() != null;

            EquipmentSlotUI equipmentSlot = GetComponent<EquipmentSlotUI>();
            if (equipmentSlot != null)
                return equipmentSlot.GetItem() != null;

            ActionSlotUI actionSlot = GetComponent<ActionSlotUI>();
            return actionSlot != null && actionSlot.GetItem() != null;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            InventorySlotUI inventorySlot = GetComponent<InventorySlotUI>();
            if (inventorySlot != null)
            {
                tooltip.GetComponent<ItemTooltip>().Setup(inventorySlot.GetItem());
                return;
            }

            EquipmentSlotUI equipmentSlot = GetComponent<EquipmentSlotUI>();
            if (equipmentSlot != null)
            {
                tooltip.GetComponent<ItemTooltip>().Setup(equipmentSlot.GetItem());
                return;
            }

            ActionSlotUI actionSlot = GetComponent<ActionSlotUI>();
            if (actionSlot != null)
                tooltip.GetComponent<ItemTooltip>().Setup(actionSlot.GetItem());
        }
    }
}