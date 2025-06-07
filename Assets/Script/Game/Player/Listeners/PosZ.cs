using Script.Game.Entity;
using Script.Utils;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class PosZ : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;
        [SerializeField] public float _z;

        private ListenerScheduler _scheduler;

        private void Start()
        {
            _scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (_scheduler != null)
            {
                _scheduler.RegisterListener(UpdatePosZ);
            }
            else
            {
                Debug.LogWarning("ListenerScheduler not found in scene.");
            }
            UpdatePosZ();
        }

        private void OnDestroy()
        {
            if (_scheduler != null)
            {
                _scheduler.UnregisterListener(UpdatePosZ);
            }
        }

        private void UpdatePosZ()
        {
            player = LocalPlayer.Instance.GetControlledEntity();
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();

            if (player != null && playerComponent != null)
            {
                _z = player.transform.position.z;
                playerComponent.PosZ = _z;
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set.");
            }
        }
    }
}