using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using GLTFast;
using Script.Game.Entity;
using Script.Game.Player;
using Script.Game.Player.Listeners;
using Script.Utils;
using Unity.VisualScripting;

namespace Script.Game.Entity.Listeners
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] public string _skin;
        [SerializeField] public string _skinState;
        [SerializeField] private int _team;
        
        [SerializeField] EntityComponent _entityComponent;

        public Transform SkinContainer;
        public GameObject CurrentSkin { get; private set; }

        private void Awake()
        {
            _skin = string.Empty;
            _skinState = "Component Initialized";
            Transform parent = transform.parent;
            if (!parent.IsUnityNull())
            {
                Transform container = parent.Find("SkinContainer");
                if (!container.IsUnityNull())
                {
                    SkinContainer = container;
                }
                else
                {
                    Log.Failure($"[Skin.cs] SkinContainer not found in parent of {gameObject.name}");
                }
            }
            else
            {
                Log.Failure($"[Skin.cs] {gameObject.name} has no parent, cannot find SkinContainer.");
            }
        }

        private void Update()
        {
            if (!_entityComponent.IsUnityNull())
            {
                if ((_entityComponent.Name + ".glb") != _skin || _entityComponent.Team != _team)
                {
                    UpdateSkin();
                }
            }
        }

        private void OnDestroy()
        {
            CurrentSkin = null;
            _skinState = "Component Destroyed";
        }

        public void UpdateSkin()
        {
            _entityComponent = gameObject.GetComponentInParent<EntityComponent>();

            if (!_entityComponent.IsUnityNull())
            {
                string name_ = _entityComponent.Name;
                var team = _entityComponent.Team;
                    
                if (name_ == "Tower" || name_ == "Nexus" || name_ == "Inhibitor")
                    if (team == 1)
                        name_ = "Blue_" + name_;
                    else if (team == 2)
                        name_ = "Red_" + name_;
                
                _ = LoadSkin(name_, team); // Appelle async sans attendre (fire-and-forget)
            }
            else
            {
                Log.Warn("Player or PlayerComponent is not set.");
            }
        }

        private async Task LoadSkin(string name_, int team_)
        {
            if (_skin != (name_ + ".glb"))
            {
                _team = team_;
                _skin = name_ + ".glb";
                _skinState = "Loading " + _skin;
                
                if (SkinContainer.childCount > 0)
                {
                    foreach (Transform child in SkinContainer)
                    {
                        Destroy(child.gameObject);
                    }
                }
                CurrentSkin = null;

                string filePath = Path.Combine(Application.streamingAssetsPath, name_ + ".glb");

                if (!File.Exists(filePath))
                {
                    Log.Failure("Skin file not found: " + filePath);
                    _skinState = "Not found " + filePath;
                    return;
                }

                var gltf = new GltfImport();
                bool success = await gltf.Load(filePath);

                if (!success)
                {
                    Log.Failure("Failed to load GLB file: " + filePath);
                    _skinState = "Failed to load " + filePath;
                    return;
                }

                GameObject skinRoot = new GameObject(name_ + "_Skin");
                skinRoot.transform.SetParent(SkinContainer, false);
                skinRoot.transform.localPosition = Vector3.zero;
                skinRoot.transform.localRotation = Quaternion.identity;
                skinRoot.transform.localScale = Vector3.one * 0.005f;

                bool instantiated = await gltf.InstantiateMainSceneAsync(skinRoot.transform);
                if (!instantiated)
                {
                    Destroy(skinRoot);
                    CurrentSkin = null;
                    Log.Failure("GLB loaded but instantiation failed.");
                    _skinState = "Failed to instantiate " + filePath;
                    return;
                }

                CurrentSkin = skinRoot;
                _skinState = "Loaded " + filePath;

                //Log.Info($"Skin {name_} loaded and instantiated.");
            }
        }
    }
}
