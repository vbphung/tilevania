using UnityEngine;

namespace Characters
{
    public abstract class InteractableNPC : MonoBehaviour
    {
        [SerializeField] GameObject interactableIcon;

        private void Start()
        {
            interactableIcon.transform.localScale = transform.localScale;
            interactableIcon.SetActive(false);
        }

        private void OnMouseEnter()
        {
            interactableIcon.SetActive(true);
        }

        private void OnMouseExit()
        {
            interactableIcon.SetActive(false);
        }

        public abstract void OnMouseDown();
    }
}