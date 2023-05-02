using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject bigCoinPrefab; // ������ ������� �������
    public GameObject smallCoinPrefab; // ������ ��������� �������

    public int totalCoins = 150; // ����� ���������� ����� �� �����
    public int bigCoins = 40; // ���������� ������� �����
    public int smallCoins = 40; // ���������� ��������� �����

    public float spawnRadius = 10f; // ������ ������ �����

    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        int coinsSpawned = 0;
        int bigCoinsSpawned = 0;
        int smallCoinsSpawned = 0;

        while (coinsSpawned < totalCoins)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0f;

            if (bigCoinsSpawned < bigCoins && Random.value < 0.5f)
            {
                Instantiate(bigCoinPrefab, spawnPosition, Quaternion.identity);
                bigCoinsSpawned++;
                coinsSpawned++;
            }
            else if (smallCoinsSpawned < smallCoins)
            {
                Instantiate(smallCoinPrefab, spawnPosition, Quaternion.identity);
                smallCoinsSpawned++;
                coinsSpawned++;
            }
        }
    }
}
