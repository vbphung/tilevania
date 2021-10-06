using UnityEngine;

using Inventories.UI;
using UI.Dragging;

namespace Inventories.Actions.UI
{
    public class ActionSlotUI : MonoBehaviour, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryIcon icon;
        [SerializeField] int index = 0;

        ActionStore actionStore;

        private void Awake()
        {
            actionStore = GameObject.FindWithTag("Player").GetComponent<ActionStore>();
            actionStore.actionStoreUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        private void Redraw()
        {
            icon.Setup(actionStore.GetActionInSlot(index), actionStore.GetNumberInSlot(index));
        }

        public InventoryItem GetItem()
        {
            return actionStore.GetActionInSlot(index) as InventoryItem;
        }

        public bool Acceptable(InventoryItem item)
        {
            ActionItem actionItem = item as ActionItem;
            if (actionItem == null)
                return false;

            ActionItem actionItemInStore = actionStore.GetActionInSlot(index);
            return actionItemInStore == null || object.ReferenceEquals(actionItemInStore, actionItem);
        }

        public void AddItems(InventoryItem item, int number)
        {
            actionStore.AddItems((ActionItem)item, index, number);
        }

        public int GetNumber()
        {
            return actionStore.GetNumberInSlot(index);
        }

        public void RemoveItems(int number)
        {
            actionStore.RemoveItems(index, number);
        }
    }
}