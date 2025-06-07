using Script.Game.Entity;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class PosY : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;
        [SerializeField] public float _y;

        private ListenerScheduler _scheduler;

        private void Start()
        {
            _scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (!_scheduler.IsUnityNull())
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
            if (!_scheduler.IsUnityNull())
            {
                _scheduler.UnregisterListener(UpdatePosZ);
            }
        }

        private void UpdatePosZ()
        {
            player = LocalPlayer.Instance.GetControlledEntity();
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();

            if (!player.IsUnityNull() && !playerComponent.IsUnityNull())
            {
                _y = player.transform.position.y;
                playerComponent.PosZ = _y;
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set.");
            }
        }
    }
}