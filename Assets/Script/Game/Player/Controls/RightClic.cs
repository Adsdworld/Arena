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
        
        [SerializeField] private NavMeshAgent _agent;
        
        public UnityEngine.Camera _mainCamera;

        [SerializeField] private GameObject playerCapsule;

        [SerializeField] private EntityComponent _entityComponent;
        
        public LayerMask layerMask;

        [SerializeField] private bool isArrived;
        [SerializeField] private bool isNearDestination;
        
        private void Awake()
        {
            _controls = new InputSystem_Actions();
            _controls.Player.RightClic.performed += OnRightClic;
        }
        
        private void Update()
        {
            if (!_agent.IsUnityNull()
                && _agent.enabled 
                && _agent.isActiveAndEnabled 
                && _agent.isOnNavMesh)
            {
                Vector3 dir = _agent.desiredVelocity;
                dir.y = 0;
                if (dir != Vector3.zero)
                {
                    //transform.rotation = Quaternion.LookRotation(dir);
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    playerCapsule.transform.rotation = Quaternion.RotateTowards(playerCapsule.transform.rotation, targetRotation, 900 * Time.deltaTime); // 21.05 anciennemen 720

                }

                if (HasReachedDestination(_agent))
                {
                    isArrived = true;
                    isNearDestination = true;
                    _entityComponent.Moving = false;
                    _agent.enabled = false;
                }
                else if (IsNearDestination(_agent, 10.0f))
                {
                    // On est proche, on peut par exemple ralentir ou préparer l’arrêt
                    // Ou basculer Moving à false plus tôt si tu veux
                    _entityComponent.Moving = false; 
                    isNearDestination = true;
                    isArrived = false; // On est pas encore arrivé mais on arrête le mouvement "plein gaz"
                }
                else
                {
                    isNearDestination = false;
                    isArrived = false;
                    _entityComponent.Moving = true;
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
            if (isArrived)
            {
                _agent.enabled = true;
            }
            isArrived = false;
            _entityComponent.Moving = true;
            if (_agent.IsUnityNull())
            {
                Debug.LogError("NavMeshAgent is not set. Please call UpdateRightClicEntityController with a valid GameObject.");
                return;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
            {
                _agent.SetDestination(hit.point);
                _entityComponent.PosXDesired = hit.point.x;
                _entityComponent.PosYDesired = hit.point.y;
                _entityComponent.PosZDesired = hit.point.z;
                ListenerScheduler.Instance.SendLocalPlayerUpdate();
                Vector3 dir = _agent.desiredVelocity;
                dir.y = 0;
                if (dir != Vector3.zero)                
                {
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    playerCapsule.transform.rotation = Quaternion.RotateTowards(playerCapsule.transform.rotation, targetRotation, 900 * Time.deltaTime);
                }

                ListenerScheduler.Instance.SendLocalPlayerUpdate();
                // penser côté java à mettre à jour les infos avec les datas x, y, z,
                // pas que l'action et as t on vraiment besoin de l'action :
                // je dirais que pour les cast et les attaques
            }
        }

        public void UpdateRightClicEntityController(GameObject gameObject)
        {
            playerCapsule = gameObject;
            _agent = gameObject.GetComponent<NavMeshAgent>();
            if (_agent.IsUnityNull())
            {
                Log.Warn("NavMeshAgent component is missing on the provided GameObject. Please ensure it has a NavMeshAgent component.");
                return;
            }
            _agent.updateRotation = false;
            _agent.acceleration = 999.0f;
            _agent.speed = gameObject.GetComponent<EntityComponent>().MoveSpeed;
            _entityComponent = gameObject.GetComponent<EntityComponent>();
        }

        public void SetMoveSpeed(float moveSpeed)
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
        
        bool HasReachedDestination(NavMeshAgent agent)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        bool IsNearDestination(NavMeshAgent agent, float nearDistanceThreshold)
        {
            if (!agent.pathPending)
            {
                // Seuil plus large que stoppingDistance pour détecter "proche"
                if (agent.remainingDistance <= nearDistanceThreshold)
                {
                    return true;
                }
            }
            return false;
        }


    }
}