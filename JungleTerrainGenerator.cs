using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleTerrainGenerator : MonoBehaviour
{
    [Header("Terrain Settings")]
    public int width = 50;
    public int length = 50;
    public float heightScale = 10f;
    public float noiseScale = 0.1f;

    [Header("Environment Objects")]
    public GameObject treePrefab;
    public GameObject plantPrefab;
    public GameObject waterfallPrefab;
    public GameObject riverPrefab;
    public int treeCount = 20;
    public int plantCount = 20;

    private Terrain terrain;
    private TerrainData terrainData;

    void Start()
    {
        GenerateTerrain();
        StartCoroutine(SpawnEnvironmentObjects());
    }

    void GenerateTerrain()
    {
        terrain = gameObject.AddComponent<Terrain>();
        terrainData = new TerrainData();
        terrain.terrainData = terrainData;
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, heightScale, length);

        float[,] heights = new float[width + 1, length + 1];
        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= length; z++)
            {
                heights[x, z] = Mathf.PerlinNoise(x * noiseScale, z * noiseScale);
            }
        }
        terrainData.SetHeights(0, 0, heights);
    }

    IEnumerator SpawnEnvironmentObjects()
    {
        yield return new WaitForSeconds(1f); // Ensure terrain spawns first

        SpawnObject(treePrefab, treeCount, new Vector3(515, -293, 857), 10);
        SpawnObject(plantPrefab, plantCount, new Vector3(511, -293, 887), 10);

        if (waterfallPrefab != null) Instantiate(waterfallPrefab, new Vector3(width / 2, heightScale / 2, length / 2), Quaternion.identity);
        if (riverPrefab != null) Instantiate(riverPrefab, new Vector3(width / 2, 0, length / 2), Quaternion.identity);
    }

    void SpawnObject(GameObject prefab, int count, Vector3 centerPosition, float range)
    {
        if (prefab == null) return;
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                centerPosition.x + Random.Range(-range, range),
                centerPosition.y,  // Set Y position to -292
                centerPosition.z + Random.Range(-range, range)
            );

            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
