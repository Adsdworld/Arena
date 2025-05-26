using UnityEngine;
using GLTFast;
using Script.Utils;
using System.IO;

namespace Script
{
    public class SummonersRiftLoader : MonoBehaviour
    {
        public string fileName = "summoner's rift.glb"; // relative to StreamingAssets

        async void Start()
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileName);
            
            Log.Info("Loading Summoner's Rift");

            var gltf = new GltfImport();
            bool success = await gltf.Load(path);

            if (success)
            {
                bool instantiated = await gltf.InstantiateMainSceneAsync(transform);
                if (instantiated)
                {
                    transform.localScale = new Vector3(2f, 2f, 2f);
                    Log.Info("Summoner's Rift loaded and instantiated successfully.");
                }
                else
                {
                    Log.Failure("Summoner's Rift loaded but instantiation failed.");
                }
            }
            else
            {
                Log.Failure("Failed to load Summoner's Rift file.");
            }
        }
    }
}