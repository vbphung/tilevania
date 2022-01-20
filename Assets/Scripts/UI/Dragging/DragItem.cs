using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Dragging
{
    public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where T : class
    {
        Vector3 startPosition;
        Transform originalParent;
        IDragSource<T> source;

        Canvas parentCanvas;

        private void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();
            source = GetComponentInParent<IDragSource<T>>();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
            originalParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(parentCanvas.transform, true);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPosition;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(originalParent, true);

            IDragDestination<T> container;
            if (!EventSystem.current.IsPointerOverGameObject())
                container = parentCanvas.GetComponent<IDragDestination<T>>();
            else
                container = GetContainer(eventData);

            if (container != null)
                DropItemIntoContainer(container);
        }

        private IDragDestination<T> GetContainer(PointerEventData eventData)
        {
            if (eventData.pointerEnter)
            {
                var container = eventData.pointerEnter.GetComponentInParent<IDragDestination<T>>();
                return container;
            }
            return null;
        }

        private void DropItemIntoContainer(IDragDestination<T> destination)
        {
            if (object.ReferenceEquals(destination, source))
                return;

            IDragContainer<T> destinationContainer = destination as IDragContainer<T>;
            IDragContainer<T> sourceContainer = source as IDragContainer<T>;

            if (destinationContainer == null || sourceContainer == null || destinationContainer.GetItem() == null || object.ReferenceEquals(destinationContainer.GetItem(), sourceContainer.GetItem()))
                AttemptSimpleTransfer(destination);
            else
                AttemptSwap(destinationContainer, sourceContainer);
        }

        private void AttemptSwap(IDragContainer<T> destination, IDragContainer<T> source)
        {
            int sourceNumber = source.GetNumber();
            T sourceItem = source.GetItem();

            int destinationNumber = destination.GetNumber();
            T destinationItem = destination.GetItem();

            source.RemoveItems(sourceNumber);
            destination.RemoveItems(destinationNumber);

            if (source.Acceptable(destinationItem) && destination.Acceptable(sourceItem))
            {
                source.AddItems(destinationItem, destinationNumber);
                destination.AddItems(sourceItem, sourceNumber);
            }
            else
            {
                source.AddItems(sourceItem, sourceNumber);
                destination.AddItems(destinationItem, destinationNumber);
            }
        }

        private void AttemptSimpleTransfer(IDragDestination<T> destination)
        {
            T item = source.GetItem();
            int number = source.GetNumber();

            if (destination.Acceptable(item))
            {
                source.RemoveItems(number);
                destination.AddItems(item, number);
            }
        }
    }
}