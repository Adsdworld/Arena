using Script.Game.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Script.Utils;

namespace Script.Input.Controls
{
    public class AiNavAgent : MonoBehaviour
    {
        private NavMeshAgent agent;
        private UnityEngine.Camera _mainCamera;

        private InputAction clickAction;
        private InputSystem_Actions inputActions;

        
        private float _GarenMoveSpeed = 7.5f;
        
        // commun à tous les champions
        private float _ChampionAcceleration = 999f;
        

        private void Start()
        {
            agent.updateRotation = false;
            agent.acceleration = _ChampionAcceleration;
            agent.speed = _GarenMoveSpeed;
        }

        private void Update()
        {
            Vector3 dir = agent.desiredVelocity;
            if (dir != Vector3.zero)
            {
                //transform.rotation = Quaternion.LookRotation(dir);
                Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 900 * Time.deltaTime); // 21.05 anciennemen 720

            }
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            _mainCamera = UnityEngine.Camera.main;

            inputActions = new InputSystem_Actions();
            clickAction = inputActions.Player.RightClic;
            clickAction.performed += OnClick;
        }

        void OnEnable()
        {
            inputActions?.Enable();
        }

        void OnDisable()
        {
            inputActions?.Disable();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (_mainCamera == null || agent == null)
            {
                Log.Info("Camera or agent is missing.");
                return;
            }
            
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
                var entity = LocalPlayer.Instance.GetControlledEntityComponent();
                entity.PosXDesired = hit.point.x;
                entity.PosZDesired = hit.point.z;
                entity.PosYDesired = hit.point.y;
            }
        }
    }
}

