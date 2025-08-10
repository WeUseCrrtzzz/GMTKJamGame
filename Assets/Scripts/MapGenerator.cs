
using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public enum SolTier { A, B, C, D }
    public SolTier currentSolTier;

    [Header("Grid Settings")]
    public int gridWidth = 25;
    public int gridHeight = 20;
    public float gridSpacing = 1f;

    [Header("Prefabs")]
    public GameObject resourcePrefab;
    public GameObject regenResourcePrefab;
    public GameObject wildThreatPrefab;

    [Header("Star Body Prefabs (2x2)")]
    public GameObject[] plantStarBodies;
    public GameObject[] monsterStarBodies;
    public GameObject[] warStarBodies;
    public GameObject[] mineralStarBodies; // [0] = -, [2] = +

    private List<Vector2Int> occupiedTiles = new();

    void Start()
    {
        currentSolTier = SolTier.B;
        GenerateMap();
    }

    public void GenerateMap()
    {
        ClearExistingTiles();
        occupiedTiles.Clear();

        // Life Zone Only
        Spawn1x1(wildThreatPrefab, 4, 8, 14, -2, gridWidth + 2);
        Spawn1x1(regenResourcePrefab, 4, 8, 14, -2, gridWidth + 2);

        // Random 3-5 planet clusters in Life Zone (spread X! and this needs modification for later)
        List<GameObject[]> clusters = new() { plantStarBodies, monsterStarBodies, warStarBodies };
        int count = Random.Range(3, 5);
        for (int i = 0; i < count; i++)
        {
            GameObject[] group = clusters[Random.Range(0, clusters.Count)];
            GameObject prefab = ChooseByTier(group);
            TryPlace2x2(prefab, 6, 14, -2, gridWidth + 2);
        }
        
        // Mineral+ in High Value
        for (int i = 0; i < 2; i++)
            TryPlace2x2(mineralStarBodies[2], 14, 20, -2, gridWidth + 2);

        // Mineral- in Low Value
        for (int i = 0; i < 2; i++)
            TryPlace2x2(mineralStarBodies[0], 0, 1, -2, gridWidth + 2);

        // Spread 1x1 resources across entire map, placed after clusters
        Spawn1x1(resourcePrefab, 20, 0, gridHeight + 2, -2, gridWidth + 2);
    }

    void ClearExistingTiles()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    void Spawn1x1(GameObject prefab, int count, int minY, int maxY, int minX, int maxX)
    {
        int tries = 0;
        while (count > 0 && tries < 500)
        {
            int x = Random.Range(minX, maxX);
            int y = Random.Range(minY, maxY);
            Vector2Int pos = new(x, y);

            if (!occupiedTiles.Contains(pos))
            {
                PlaceAt(prefab, pos);
                occupiedTiles.Add(pos);
                MarkBuffer(pos);
                count--;
            }
            tries++;
        }
    }

    void TryPlace2x2(GameObject prefab, int minY, int maxY, int minX, int maxX)
    {
        int tries = 0;
        while (tries < 100)
        {
            int x = Random.Range(minX, maxX);
            int y = Random.Range(minY, maxY);
            if (x >= gridWidth - 1 || y >= gridHeight - 1) { tries++; continue; }

            Vector2Int[] cluster = {
                new(x, y), new(x + 1, y),
                new(x, y + 1), new(x + 1, y + 1)
            };

            if (AllFreeWithBuffer(cluster))
            {
                Vector3 worldPos = GridToWorld(x, y);
                Instantiate(prefab, worldPos, Quaternion.identity, transform);
                foreach (var tile in cluster)
                {
                    occupiedTiles.Add(tile);
                    MarkBuffer(tile);
                }
                return;
            }
            tries++;
        }
    }

    bool AllFreeWithBuffer(Vector2Int[] cluster)
    {
        foreach (var tile in cluster)
        {
            if (occupiedTiles.Contains(tile)) return false;
            foreach (var offset in SurroundingOffsets())
                if (occupiedTiles.Contains(tile + offset)) return false;
        }
        return true;
    }

    void MarkBuffer(Vector2Int center)
    {
        foreach (var offset in SurroundingOffsets())
            occupiedTiles.Add(center + offset);
    }

    List<Vector2Int> SurroundingOffsets()
    {
        List<Vector2Int> offsets = new();
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                offsets.Add(new Vector2Int(dx, dy));
        return offsets;
    }

    Vector3 GridToWorld(int x, int y)
    {
        float offsetX = (gridWidth - 1) * gridSpacing / 2f;
        float offsetZ = (gridHeight - 1) * gridSpacing / 2f;
        return new Vector3(x * gridSpacing - offsetX, 0f, y * gridSpacing - offsetZ);
    }

    GameObject ChooseByTier(GameObject[] options)
    {
        return currentSolTier switch
        {
            SolTier.A => options[2],
            SolTier.B => options[1],
            SolTier.C => options[1],
            SolTier.D => options[0],
            _ => options[0],
        };
    }

    void PlaceAt(GameObject prefab, Vector2Int pos)
    {
        Instantiate(prefab, GridToWorld(pos.x, pos.y), Quaternion.identity, transform);
    }
}
