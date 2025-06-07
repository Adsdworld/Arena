using Script.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class RightClic : MonoBehaviour
    {
        private InputSystem_Actions _controls;
        
        private static NavMeshAgent _agent;
        
        public UnityEngine.Camera _mainCamera;
        
        private void Awake()
        {
            _controls = new InputSystem_Actions();
            _controls.Player.RightClic.performed += OnRightClic;
        }

        private void Update()
        {
            
        }

        private void OnEnable()
        {
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        private void OnRightClic(InputAction.CallbackContext context)
        {
            if (_agent == null)
            {
                Debug.LogError("NavMeshAgent is not set. Please call UpdateRightClicEntityController with a valid GameObject.");
                return;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
                Quaternion targetRotation = Quaternion.LookRotation(_agent.desiredVelocity);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 900 * Time.deltaTime);
            }
        }

        public static void UpdateRightClicEntityController(GameObject gameObject)
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
            if (_agent != null)
            {
                _agent.updateRotation = false;
                _agent.acceleration = 999.0f; // Acceleration for all champions
                _agent.speed = 1.0f;
            }
        }

        public static void SetMoveSpeed(float moveSpeed)
        {
            if (_agent == null)
            {
                Debug.LogError("NavMeshAgent is not set. Please call UpdateRightClicEntityController with a valid GameObject.");
                return;
            }

            if (moveSpeed < 0.0f)
            {
                moveSpeed = 0.0f; // Ensure speed is not negative
            }
            _agent.speed = moveSpeed;
        }
    }
}