using Script.Game.Entity;
using Script.Utils;
using UnityEngine;

namespace Script.Game.Player.Listeners
{
    public class PosZ : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public EntityComponent playerComponent;

        private ListenerScheduler scheduler;

        private void Start()
        {
            scheduler = FindFirstObjectByType<ListenerScheduler>();
            if (scheduler != null)
            {
                scheduler.RegisterListener(UpdatePosZ);
            }
            else
            {
                Debug.LogWarning("ListenerScheduler not found in scene.");
            }
            UpdatePosZ();
        }

        private void OnDestroy()
        {
            if (scheduler != null)
            {
                scheduler.UnregisterListener(UpdatePosZ);
            }
        }

        private void UpdatePosZ()
        {
            player = LocalPlayer.Instance.ControlledEntity;
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();

            if (player != null && playerComponent != null)
            {
                float z = player.transform.position.z;
                playerComponent.PosZ = z;
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set.");
            }
        }
    }
}