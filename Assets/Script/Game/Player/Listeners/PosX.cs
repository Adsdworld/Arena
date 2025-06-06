using Script.Game.Entity;
using Script.Utils;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class PosX : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;

        private ListenerScheduler scheduler;

        private void Start()
        {
            scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (scheduler != null)
            {
                scheduler.RegisterListener(UpdatePosX);
            }
            else
            {
                Debug.LogWarning("ListenerScheduler not found in scene.");
            }
            UpdatePosX();
        }

        private void OnDestroy()
        {
            if (scheduler != null)
            {
                scheduler.UnregisterListener(UpdatePosX);
            }
        }

        private void UpdatePosX()
        {
            player = LocalPlayer.Instance.ControlledEntity;
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();

            if (player != null && playerComponent != null)
            {
                float x = player.transform.position.x;
                playerComponent.PosX = x;
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set.");
            }
        }
    }
}