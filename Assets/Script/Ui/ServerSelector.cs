using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Ui
{
    public class ServerSelector : MonoBehaviour
    {
        public GameObject menuUI; // Le panneau UI à activer/désactiver
        public bool isVisible;

        public InputSystem_Actions inputActions;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            inputActions.UI.ToggleMenu.performed += OnToggleMenu;
        }

        private void OnEnable()
        {
            inputActions.UI.Enable();
        }

        private void OnDisable()
        {
            inputActions.UI.Disable();
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