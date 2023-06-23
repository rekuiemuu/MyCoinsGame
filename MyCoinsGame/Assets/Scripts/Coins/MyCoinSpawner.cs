using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MyCoinSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject bigCoinPrefab;
    [SerializeField] private GameObject smallCoinPrefab;
    [SerializeField] private int bigCoinCount = 10;
    [SerializeField] private int smallCoinCount = 10;
    [SerializeField] private float spawnRadius = 43f;

    private void Start()
    {
        SpawnCoins(bigCoinPrefab, bigCoinCount);
        SpawnCoins(smallCoinPrefab, smallCoinCount);
    }

    private void SpawnCoins(GameObject prefab, int count)
    {
        

        for (int i = 0; i < count; i++)
        {
            Vector3 position = transform.position + Random.insideUnitSphere * spawnRadius; 
            position.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 1f);
            if (colliders.Length == 0 && tilemap.GetTile(Vector3Int.RoundToInt(position)) != null)
            {
                Instantiate(prefab, position, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }
}
