using UnityEngine;
using System.Collections;

public class SunProjectileSpawner : MonoBehaviour
{
    public MapGenerator map;                  // Assign your MapGenerator in the Inspector
    public GameObject sunProjectilePrefab;    

    [Header("Spawn Timing")]
    public float spawnInterval = 30f;         // Seconds between spawns

    [Header("High Value Zone (25x20 default)")]
    public int highMinY = 14;                  // Matches your high-value Y range
    public int highMaxY = 19;

    private Coroutine loop;

    void OnEnable()
    {
        loop = StartCoroutine(SpawnLoop());
    }

    void OnDisable()
    {
        if (loop != null) StopCoroutine(loop);
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Only spawn if Sol tier is A
            if (map != null && map.currentSolTier == MapGenerator.SolTier.A)
            {
                SpawnOne();
            }
        }
    }

    void SpawnOne()
    {
        if (map == null || sunProjectilePrefab == null) return;

        // Pick a random tile in the high-value zone
        int x = Random.Range(0, map.gridWidth);
        int y = Random.Range(highMinY, highMaxY + 1);

        // Convert grid coords to world position (same as MapGenerator.GridToWorld)
        float offsetX = (map.gridWidth - 1) * map.gridSpacing / 2f;
        float offsetZ = (map.gridHeight - 1) * map.gridSpacing / 2f;
        Vector3 worldPos = new Vector3(x * map.gridSpacing - offsetX, 0f, y * map.gridSpacing - offsetZ);

        // Spawn projectile
        var go = Instantiate(sunProjectilePrefab, worldPos, Quaternion.identity);

        // Match its grid step to the map's spacing
        var proj = go.GetComponent<SunProjectile>();
        if (proj != null)
            proj.gridStep = map.gridSpacing;
    }
}
