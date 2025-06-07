using Script.Game.Entity;
using Script.Utils;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class RotationY : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;

        private ListenerScheduler _scheduler;

        private void Start()
        {
            _scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (_scheduler != null)
            {
                _scheduler.RegisterListener(UpdateRotationY);
            }
            else
            {
                Debug.LogWarning("ListenerScheduler not found in scene.");
            }
            UpdateRotationY();
        }

        private void OnDestroy()
        {
            if (_scheduler != null)
            {
                _scheduler.UnregisterListener(UpdateRotationY);
            }
        }
        
        private void UpdateRotationY()
        {
            player = LocalPlayer.Instance.GetControlledEntity();
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();
            if (player != null && playerComponent != null)
            {
                float yRotation = player.transform.eulerAngles.y;
                playerComponent.RotationY = yRotation;
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set");
            }
        }
    }
}