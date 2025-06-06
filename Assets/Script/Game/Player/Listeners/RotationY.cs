using Script.Game.Entity;
using Script.Utils;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class RotationY : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;

        private ListenerScheduler scheduler;

        private void Start()
        {
            scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (scheduler != null)
            {
                scheduler.RegisterListener(UpdateRotationY);
            }
            else
            {
                Debug.LogWarning("ListenerScheduler not found in scene.");
            }
            UpdateRotationY();
        }

        private void OnDestroy()
        {
            if (scheduler != null)
            {
                scheduler.UnregisterListener(UpdateRotationY);
            }
        }
        
        private void UpdateRotationY()
        {
            player = LocalPlayer.Instance.ControlledEntity;
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