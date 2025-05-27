using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicControls : MonoBehaviour
{
    public bool isPress;
    
    private InputSystem_Actions controls;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPress = false;
    }

    private void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Interact.performed += OnInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        isPress = !isPress;
    }
}
