using Script.Game.Entity;
using Script.Game.Player.Listeners;
using Script.Network.Message;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class RightClic : MonoBehaviour
    {
        private InputSystem_Actions _controls;
        
        [SerializeField] private static NavMeshAgent _agent;
        
        public UnityEngine.Camera _mainCamera;
        
        private void Awake()
        {
            _controls = new InputSystem_Actions();
            _controls.Player.RightClic.performed += OnRightClic;
        }
        
        private void Update()
        {
            if (!_agent.IsUnityNull())
            {
                Vector3 dir = _agent.desiredVelocity;
                if (dir != Vector3.zero)
                {
                    //transform.rotation = Quaternion.LookRotation(dir);
                    Quaternion targetRotation = Quaternion.LookRotation(_agent.desiredVelocity);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 900 * Time.deltaTime); // 21.05 anciennemen 720

                }
            }
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
            if (_agent.IsUnityNull())
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

                Message message = ListenerScheduler.Instance.CreateMessage();
                // penser côté java à mettre à jour les infos avec les datas x, y, z,
                // pas que l'action et as t on vraiment besoin de l'action :
                // je dirais que pour les cast et les attaques
            }
        }

        public void UpdateRightClicEntityController(GameObject gameObject)
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
            if (_agent.IsUnityNull())
            {
                Log.Warn("NavMeshAgent component is missing on the provided GameObject. Please ensure it has a NavMeshAgent component.");
                return;
            }
            _agent.updateRotation = false;
            _agent.acceleration = 999.0f;
            _agent.speed = gameObject.GetComponent<EntityComponent>().MoveSpeed;
        }

        public static void SetMoveSpeed(float moveSpeed)
        {
            if (_agent.IsUnityNull())
            {
                Debug.LogError("NavMeshAgent is not set. Please call UpdateRightClicEntityController with a valid GameObject.");
                return;
            }

            if (moveSpeed < 0.0f)
            {
                moveSpeed = 0.0f; // Ensure speed is not negative
                Log.Warn("Move speed cannot be negative. Setting to 0.");
            }
            _agent.speed = moveSpeed;
        }
    }
}