using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SpawnableItem
{
    public GameObject itemPrefab;
    [Range(0f, 100f)]
    public float spawnPercentage;
}
public class ItemSpawner : MonoBehaviour
{
    public SpawnableItem[] spawnableItems;
    public float spawnRadius = 10f;
    public int firstSpawnInit = 3;

    public float spawnInterval = 10f;
    public int maximumSpawnItems = 20;
    private float nextSpawnTime;
    private int currentSpawnCount = 1;
    private float spawnCountModifier = 0.01f; // Increase spawn count by 1% every second
    
    void Start()
    {

        for(int i = 0; i <= firstSpawnInit; i++)
        {
           SpawnItems();
        }

        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            currentSpawnCount = Mathf.Clamp(currentSpawnCount, 0, maximumSpawnItems); // Limit the currentSpawnCount to a maximum of 20
            SpawnItems();
            UpdateSpawnCount();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnItems()
    {
        int itemsToSpawn = Mathf.Min(currentSpawnCount, 20); // Limit the items to spawn to a maximum of 20

        for (int i = 0; i < itemsToSpawn; i++)
        {
            float randomPercentage = Random.Range(0f, 100f);
            SpawnableItem selectedItem = null;

            // Choose an item to spawn based on percentage
            foreach (SpawnableItem item in spawnableItems)
            {
                if (randomPercentage <= item.spawnPercentage)
                {
                    selectedItem = item;
                    break;
                }

                randomPercentage -= item.spawnPercentage;
            }

            if (selectedItem != null)
            {
                Vector3 spawnPosition = Random.onUnitSphere * spawnRadius;
                Instantiate(selectedItem.itemPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    void UpdateSpawnCount()
    {
        float timeElapsed = Time.time;
        float spawnCountModifierPercent = spawnCountModifier * 100f;

        if (timeElapsed % 60f == 0f) // Every 2 minutes
        {
            float spawnDecreasePercentage = 20f * Mathf.Sin(timeElapsed * Mathf.PI / 60f); // Sinusoidal decrease by 10%
            spawnCountModifierPercent -= spawnDecreasePercentage;

            // Clamp spawnCountModifierPercent to a minimum of 0
            spawnCountModifierPercent = Mathf.Max(spawnCountModifierPercent, 0f);
        }

        currentSpawnCount = Mathf.RoundToInt(currentSpawnCount * (1f + spawnCountModifierPercent));

        Debug.Log("Current Spawn Count: " + currentSpawnCount);
    }
}

