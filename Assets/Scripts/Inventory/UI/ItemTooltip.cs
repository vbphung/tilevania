using UnityEngine;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] Text itemName;

        public void Setup(InventoryItem item)
        {
            itemName.text = item.name;
        }
    }
}