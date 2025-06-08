using UnityEngine;

public class HideTerrain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrain.drawHeightmap = false;
            terrain.drawTreesAndFoliage = false;
        }
        else
        {
            Debug.LogWarning("No Terrain component found on this GameObject.");
        }
    }
}