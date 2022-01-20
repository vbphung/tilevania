using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class TooltipSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject tooltipPrefab = null;
        [SerializeField] Vector3 tooltipOffset;

        GameObject tooltip = null;

        private void OnDestroy()
        {
            ClearTooltip();
        }

        private void OnDisable()
        {
            ClearTooltip();
        }

        public abstract void UpdateTooltip(GameObject tooltip);

        public abstract bool CanCreateTooltip();

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Canvas parentCanvas = GetComponentInParent<Canvas>();

            if (tooltip && !CanCreateTooltip())
                ClearTooltip();

            if (!tooltip && CanCreateTooltip())
                tooltip = Instantiate(tooltipPrefab, parentCanvas.transform);

            if (tooltip)
            {
                UpdateTooltip(tooltip);
                tooltip.transform.position = Input.mousePosition + tooltipOffset;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            ClearTooltip();
        }

        private void ClearTooltip()
        {
            if (tooltip)
                Destroy(tooltip.gameObject);
        }
    }
}