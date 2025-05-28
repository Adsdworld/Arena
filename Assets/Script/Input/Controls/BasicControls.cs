using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Input.Controls
{
    public class BasicControls : MonoBehaviour
    {
        private InputSystem_Actions inputActions;
        
        private void Awake()
        {
            inputActions = new InputSystem_Actions();
            inputActions.Player.Clic.performed += OnAction;
        }
        
        private void OnEnable()
        {
            inputActions?.Enable();
        }
        
        private void OnDisable()
        {
            inputActions?.Disable();
        }
        
        private void OnAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Action performed!");
            }
        }
        
    }
}
