using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Ui
{
    public class ServerSelector : MonoBehaviour
    {
        public GameObject menuUI; // Le panneau UI à activer/désactiver
        public bool isVisible;

        private InputSystem_Actions controls;

        private void Awake()
        {
            controls = new InputSystem_Actions();
            controls.ServerSelector.ToggleMenu.performed += OnToggleMenu;
        }

        private void OnEnable()
        {
            controls.ServerSelector.Enable();
        }

        private void OnDisable()
        {
            controls.ServerSelector.Disable();
        }

        private void Start()
        {
            if (menuUI != null)
            {
                menuUI.SetActive(false); // On le cache au début
                isVisible = false;
            }
        }

        private void OnToggleMenu(InputAction.CallbackContext context)
        {
            isVisible = !isVisible;
            menuUI.SetActive(isVisible);
        }
    }
}