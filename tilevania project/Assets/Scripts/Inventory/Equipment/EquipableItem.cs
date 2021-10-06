using UnityEngine;

namespace Inventories.Equipments
{
    [CreateAssetMenu(fileName = "New Equiptable Item", menuName = "Inventory Item/Equiptable Item", order = 0)]
    public class EquipableItem : InventoryItem
    {
        [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

        public EquipLocation GetEquipLocation()
        {
            return equipLocation;
        }
    }
}