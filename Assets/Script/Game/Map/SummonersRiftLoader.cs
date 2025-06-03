using System.IO;
using GLTFast;
using UnityEngine;
using Script.Utils;

namespace Script.Game.Map
{
    public class SummonersRiftLoader : MonoBehaviour
    {
        public string fileName = "summoner's rift.glb"; // relative to StreamingAssets

        async void Start()
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileName);

            var gltf = new GltfImport();
            bool success = await gltf.Load(path);

            if (success)
            {
                bool instantiated = await gltf.InstantiateMainSceneAsync(transform);
                if (instantiated)
                {
                    transform.localScale = new Vector3(20f, 20f, 20f);
                    Log.Info("GLB summoner rift loaded and instantiated successfully.");
                }
                else
                {
                    Log.Failure("GLB summoner rift loaded but instantiation failed.");
                }
            }
            else
            {
                Log.Failure("Failed to load GLB summoner rift file.");
            }
        }
    }
}
