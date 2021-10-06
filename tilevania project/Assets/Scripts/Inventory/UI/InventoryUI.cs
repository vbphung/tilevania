using UnityEngine;

namespace Inventories.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventorySlotUI inventorySlotPrefab;

        Inventory inventory;

        private void Awake()
        {
            inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            inventory.inventoryUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            for (int i = 0; i < inventory.GetSize(); ++i)
            {
                InventorySlotUI inventorySlot = Instantiate(inventorySlotPrefab, transform);
                inventorySlot.Setup(inventory, i);
            }
        }
    }
}