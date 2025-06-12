/*using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using GLTFast;
using Script.Utils;
using Unity.VisualScripting;

namespace Script.Game.Entity.Listeners
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private float _x;
        [SerializeField] private float _y;
        [SerializeField] private float _z;
        [SerializeField] private float _desiredX;
        [SerializeField] private float _desiredY;
        [SerializeField] private float _desiredZ;
        
        [SerializeField] private EntityComponent _entityComponent;
        [SerializeField] private GameObject _entityCapsule;

        [SerializeField] private string _state;
        
        private void Awake()
        {
            _x = float.NaN;
            _y = float.NaN;
            _z = float.NaN;
            _desiredX = float.NaN;
            _desiredY = float.NaN;
            _desiredZ = float.NaN;
            _state = "Component Initialized";
            Transform parent = transform.parent;
            if (!parent.IsUnityNull())
            {
                _entityCapsule = parent;
            }
            else
            {
                Log.Failure("Game object parent is null.");
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
            if (SkinContainer.childCount > 0)
            {
                foreach (Transform child in SkinContainer)
                {
                    Destroy(child.gameObject);
                }
            }
            CurrentSkin = null;
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
                skinRoot.transform.localPosition = new Vector3(_entityComponent.PosSkinX, _entityComponent.PosSkinY, _entityComponent.PosSkinZ);
                skinRoot.transform.localRotation = Quaternion.identity;
                skinRoot.transform.localScale = Vector3.one * _entityComponent.SkinScale;

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
                
                UpDateEntitySkinControllers(skinRoot);

                //Log.Info($"Skin {name_} loaded and instantiated.");
            }
        }

        private void UpDateEntitySkinControllers(GameObject aGameObject)
        {
            gameObject.GetComponent<SkinAnimations>().UpdateAnimationEntitySkinController(aGameObject, _entityComponent);
        }
    }
}
*/