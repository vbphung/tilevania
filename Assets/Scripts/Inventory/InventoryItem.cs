using UnityEngine;

namespace Inventories
{
    public abstract class InventoryItem : ScriptableObject
    {
        [SerializeField] Sprite icon = null;
        [SerializeField] bool stackable = false;
        [SerializeField] float price;
        [SerializeField] ItemCategory category = ItemCategory.None;

        public Sprite GetIcon()
        {
            return icon;
        }

        public bool IsStackable()
        {
            return stackable;
        }

        public float GetPrice()
        {
            return price;
        }

        public ItemCategory GetCategory()
        {
            return category;
        }
    }
}