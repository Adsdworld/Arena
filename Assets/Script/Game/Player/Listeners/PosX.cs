using Script.Game.Entity;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class PosX : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;

        [SerializeField] public float _x;

        private ListenerScheduler _scheduler;

        private void Start()
        {
            _scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (!_scheduler.IsUnityNull())
            {
                _scheduler.RegisterListener(UpdatePosX);
            }
            else
            {
                Debug.LogWarning("ListenerScheduler not found in scene.");
            }

            UpdatePosX();
        }

        private void OnDestroy()
        {
            if (!_scheduler.IsUnityNull())
            {
                _scheduler.UnregisterListener(UpdatePosX);
            }
        }

        private void UpdatePosX()
        {
            player = LocalPlayer.Instance.GetControlledEntity();
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();

            if (!player.IsUnityNull() && !playerComponent.IsUnityNull())
            {
                _x = player.transform.position.x;
                playerComponent.PosX = _x;
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set.");
            }
        }
    }
}