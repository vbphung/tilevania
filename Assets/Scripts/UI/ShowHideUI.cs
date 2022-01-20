using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject uIContainer = null;
        [SerializeField] bool startingState = false;

        void Start()
        {
            uIContainer.SetActive(startingState);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
                Toggle();
        }

        private void Toggle()
        {
            uIContainer.SetActive(!uIContainer.activeSelf);
        }
    }
}