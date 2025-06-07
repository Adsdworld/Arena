using Script.Game.Entity;
using Script.Utils;
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
            if (_scheduler != null)
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
            if (_scheduler != null)
            {
                _scheduler.UnregisterListener(UpdatePosX);
            }
        }

        private void UpdatePosX()
        {
            player = LocalPlayer.Instance.GetControlledEntity();
            playerComponent = LocalPlayer.Instance.GetControlledEntityComponent();

            if (player != null && playerComponent != null)
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