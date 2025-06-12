using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using GLTFast;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace Script.Game.Entity.Listeners
{
    public class Move : MonoBehaviour
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
            }
            else
            {
                Log.Failure("Game object parent is null.");
            }
        }

        private void Update()
        {
            if (_entityComponent.IsUnityNull()) return;
            if (_entityComponent.Moving)
            {
                if (!_agent.IsUnityNull()
                    && _agent.enabled
                    && _agent.isActiveAndEnabled
                    && _agent.isOnNavMesh)
                {
                    _state = "Agent is moving";
                    _desiredX = _entityComponent.PosXDesired;
                    _desiredY = _entityComponent.PosYDesired;
                    _desiredZ = _entityComponent.PosZDesired;
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
                var pos = new Vector3(_entityComponent.PosX, _entityComponent.PosY, _entityComponent.PosZ);
                if ((_entityCapsule.transform.position - pos).sqrMagnitude > 0.01f)
                {
                    _state = "Teleporting entity to position";
                    _x = _entityComponent.PosX;
                    _y = _entityComponent.PosY;
                    _z = _entityComponent.PosZ;
                    _entityCapsule.transform.position = pos;
                }
            }
        }
    }
}