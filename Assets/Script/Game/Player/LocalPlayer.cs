using System;
using Script.Camera;
using Script.Game.Entity;
using Script.Game.Player.Controls;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player
{
    public class LocalPlayer : MonoBehaviour
    {
        [SerializeField]
        public static string defaultControlledEntityId = "Entity_default"; //

        [SerializeField] private string ControlledEntityId;

        [SerializeField] private GameObject ControlledEntity;
        
        [SerializeField] public GameNameEnum GameName;

        private bool _stopLog;

        
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
            UpDateControlledEntity();
        }

        public void Start()
        {
             UpDateControlledEntity();
        }

        private void Update()
        {
            UpDateControlledEntity();
            
         }
        
        private void UpDateControlledEntity()
        {
            // Si ControlledEntity est valide ET que son ID correspond, on ne fait rien (early return)
            if (!ControlledEntity.IsUnityNull() && GetControlledEntityId() == ControlledEntity.name)
                return;

            // Si ControlledEntityId est vide, on lui donne la valeur par défaut
            if (string.IsNullOrEmpty(ControlledEntityId))
            {
                ControlledEntityId = defaultControlledEntityId;
            }

            GameObject entity = GameObject.Find(ControlledEntityId);
            if (!entity.IsUnityNull())
            {
                ControlledEntity = entity;
                UpDateEntityControllers();
                _stopLog = false;
            }
            else
            {
                if (!_stopLog)
                {
                    Log.Warn($"Controlled entity with name {ControlledEntityId} not found.");
                    _stopLog = !_stopLog;
                }
            }
        }


        private void UpDateEntityControllers()
        {
            GameObject.FindFirstObjectByType<MainCamera>().UpdateMainCameraControlledEntity(ControlledEntity);
            GameObject.FindAnyObjectByType<RightClic>().UpdateRightClicEntityController(ControlledEntity);
            GameObject.FindAnyObjectByType<Q>().UpdateQEntityController(ControlledEntity);
            GameObject.FindAnyObjectByType<W>().UpdateWEntityController(ControlledEntity);
            GameObject.FindAnyObjectByType<E>().UpdateEEntityController(ControlledEntity);
            GameObject.FindAnyObjectByType<R>().UpdateREntityController(ControlledEntity);
            
        }

        public void SetControlledEntityId(string id)
        {
            ControlledEntityId = id;
            UpDateControlledEntity();
        }

        public string GetControlledEntityId()
        {
            return ControlledEntityId;
        }
        
        private void SetControlledEntity(GameObject gameObject)
        {
            ControlledEntity = gameObject;
        }

        public GameObject GetControlledEntity()
        {
            return ControlledEntity;
        }
        
        public EntityComponent GetControlledEntityComponent()
        {
            if (ControlledEntity.IsUnityNull())
            {
                Log.Warn("ControlledEntity is null in GetControlledEntityComponent()");
                return null;
            }

            var component = ControlledEntity.GetComponent<EntityComponent>();
            if (component.IsUnityNull())
            {
                Log.Warn("EntityComponent not found on ControlledEntity");
            }

            return component;
        }
    }
}