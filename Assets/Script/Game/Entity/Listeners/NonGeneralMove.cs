using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using GLTFast;
using Script.Game.Player;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace Script.Game.Entity.Listeners
{
    public class NonGeneralMove : MonoBehaviour
    {
        [SerializeField] private float _x;
        [SerializeField] private float _y;
        [SerializeField] private float _z;
        [SerializeField] private float _desiredX;
        [SerializeField] private float _desiredY;
        [SerializeField] private float _desiredZ;

        [SerializeField] private NavMeshAgent _agent;

        [SerializeField] private EntityComponent _entityComponent;
        [SerializeField] private GameObject _entityCapsule;

        [SerializeField] private string _state;

        private void Awake()
        {
            _x = float.NaN;
            _y = float.NaN;
            _z = float.NaN;
            _desiredX = float.NaN;
            _desiredY = float.NaN;
            _desiredZ = float.NaN;
            _state = "Component Initialized";
            Transform parent = transform.parent;
            if (!parent.IsUnityNull())
            {
                _entityCapsule = parent.gameObject;
                _entityComponent = _entityCapsule.GetComponent<EntityComponent>();
                _agent = _entityCapsule.GetComponent<NavMeshAgent>();
                _agent.updateRotation = false;
                _agent.acceleration = 999.0f;
            }
            else
            {
                Log.Failure("Game object parent is null.");
            }
        }

        private void Update()
        {
            if (_entityComponent.IsUnityNull()) return;
            if ("Entity_" + _entityComponent.Id == LocalPlayer.Instance.GetControlledEntityId()) return;
            
            if (!_entityComponent.HasArrived)
            {
                if (_agent.IsUnityNull()) return;
                if (_entityComponent.NavMeshAgent_.Enabled)
                {
                    if (!_agent.enabled) _agent.enabled = true;
                }
                if (_agent.isActiveAndEnabled
                    && _agent.isOnNavMesh)
                {
                    _state = "Agent is moving";
                    _desiredX = _entityComponent.PosXDesired;
                    _desiredY = _entityComponent.PosYDesired;
                    _desiredZ = _entityComponent.PosZDesired;
                    _agent.speed = _entityComponent.MoveSpeed;
                    var posDesired = new Vector3(_entityComponent.PosXDesired, _entityComponent.PosYDesired,
                        _entityComponent.PosZDesired);
                    if ((posDesired - _agent.destination).sqrMagnitude > 0.01f)
                        _agent.SetDestination(posDesired);

                    Vector3 dir = _agent.desiredVelocity;
                    dir.y = 0;
                    if (dir != Vector3.zero)
                    {
                        //transform.rotation = Quaternion.LookRotation(dir);
                        Quaternion targetRotation = Quaternion.LookRotation(dir);
                        _entityCapsule.transform.rotation = Quaternion.RotateTowards(_entityCapsule.transform.rotation,
                            targetRotation, 900 * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (_agent.enabled)
                {
                    _agent.enabled = false;
                    _state = "Agent disabled. Entity has arrived";
                }
                
                var pos = new Vector3(_entityComponent.PosX, _entityComponent.PosY, _entityComponent.PosZ);
                if (_entityCapsule.transform.position != pos)
                {
                    _entityCapsule.transform.position = pos;
                    _x = _entityComponent.PosX;
                    _y = _entityComponent.PosY;
                    _z = _entityComponent.PosZ;
                    _state = "Entity position updated";
                }

                var rot = Quaternion.Euler(0f, _entityComponent.RotationY, 0f);
                if (Quaternion.Angle(_entityCapsule.transform.rotation, rot) > 0.01f)
                {
                    _entityCapsule.transform.rotation = rot;
                    _state = "Entity rotation updated";
                }
            }
        }
    }
}