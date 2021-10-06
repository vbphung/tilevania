using UnityEngine;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class InventoryIcon : MonoBehaviour
    {
        [SerializeField] Image itemIcon;
        [SerializeField] Text itemNumber;

        public void Setup(InventoryItem item, int number)
        {
            if (item == null)
            {
                itemIcon.enabled = false;
                itemNumber.enabled = false;
                return;
            }

            itemIcon.enabled = true;
            itemIcon.sprite = item.GetIcon();

            if (number <= 1)
            {
                itemNumber.enabled = false;
                return;
            }

            itemNumber.enabled = true;
            itemNumber.text = number.ToString();
        }
    }
}