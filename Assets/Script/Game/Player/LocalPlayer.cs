using System;
using Script.Game.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player
{
    public class LocalPlayer : MonoBehaviour
    {
        [SerializeField]
        public static string defaultControlledEntityId = "Entity_default"; //

        [SerializeField] public string ControlledEntityId;

        [SerializeField] public GameObject ControlledEntity;
        
        [SerializeField] public GameNameEnum GameName;

        
        [SerializeField]
        public int Team { get; private set; }
        
        private static LocalPlayer instance;

        public static LocalPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("EntityManager instance is null! Make sure EntityManager is present in the scene.");
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
             UpDateControlledEntity();
        }

        private void Update()
        {
            UpDateControlledEntity();
            
            /*// Tester la touche Q (juste pressée)
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var entity = LocalPlayer.Instance.GetControlledEntityComponent();
                if (entity != null)
                {
                    entity.CooldownQStart = now;
                    entity.CooldownQEnd = now + 5000; // cooldown 5s
                }
            }

            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var entity = LocalPlayer.Instance.GetControlledEntityComponent();
                if (entity != null)
                {
                    entity.CooldownWStart = now;
                    entity.CooldownWEnd = now + 5000; // cooldown 5s
                }
            }
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var entity = LocalPlayer.Instance.GetControlledEntityComponent();
                if (entity != null)
                {
                    entity.CooldownEStart = now;
                    entity.CooldownEEnd = now + 5000; // cooldown 5s
                }
            }
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var entity = LocalPlayer.Instance.GetControlledEntityComponent();
                if (entity != null)
                {
                    entity.CooldownRStart = now;
                    entity.CooldownREnd = now + 5000; // cooldown 5s
                }
            }*/
         }
        
        private void UpDateControlledEntity()
        {
            if (ControlledEntity == null || GetControlledEntityId() != ControlledEntity.name)
            {
                if (string.IsNullOrEmpty(ControlledEntityId))
                {
                    ControlledEntityId = defaultControlledEntityId;
                }

                GameObject entity = GameObject.Find(ControlledEntityId);
                if (entity != null)
                {
                    ControlledEntity = entity;
                }
                else
                {
                    Debug.LogWarning($"Controlled entity with ID {ControlledEntityId} not found.");
                }
            }

        }

        public void SetControlledEntityId(string id)
        {
            ControlledEntityId = id;
        }

        public string GetControlledEntityId()
        {
            return ControlledEntityId;
        }
        
        public void SetControlledEntity(GameObject gameObject)
        {
            ControlledEntity = gameObject;
        }

        public GameObject GetControlledEntity()
        {
            return ControlledEntity;
        }
        
        public EntityComponent GetControlledEntityComponent()
        {
            if (ControlledEntity == null)
            {
                Debug.LogWarning("ControlledEntity is null in GetControlledEntityComponent()");
                return null;
            }

            var component = ControlledEntity.GetComponent<EntityComponent>();
            if (component == null)
            {
                Debug.LogWarning("EntityComponent not found on ControlledEntity");
            }

            return component;
        }
    }
}