using Script.Utils;
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
            controls.Player.Interact.performed += OnToggleMenu;
        }

        private void OnEnable()
        {
            Log.Info("Enabling UI controls");
            controls.UI.Enable();
        }

        private void OnDisable()
        {
            controls.UI.Disable();
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
            Log.Info("ToggleMenu action performed");
            isVisible = !isVisible;
            menuUI.SetActive(isVisible);
        }
        
        void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Debug.Log("Escape key detected in Update()");
            }
        }

    }
}