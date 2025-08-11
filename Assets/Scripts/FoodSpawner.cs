using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public GameObject[] foodPrefabs;

    public float spawnInterval = 5f;
    private float timer = 0f;

    public int maxFoodOnMap = 1;
    private List<GameObject> activeFoods = new List<GameObject>();

    private float gameTime = 0f;
    void Update()
    {
        gameTime += Time.deltaTime;
        timer += Time.deltaTime;

        if (gameTime >= 15f)
        {
            maxFoodOnMap = 2;
        }

        if (activeFoods.Count < maxFoodOnMap && timer >= spawnInterval)
        {
            SpawnFood();
            timer = 0f;
        }
        activeFoods.RemoveAll(food => food == null);
    }

    void SpawnFood()
    {
        int index = Random.Range(0, foodPrefabs.Length);
        GameObject foodToSpawn = foodPrefabs[index];

        Bounds bounds = gridArea.bounds;
        Vector3 spawnPos;

        do
        {
            float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            spawnPos = new Vector3(x, y, 0f);
        }
        while (PositionBlocked(spawnPos));

        GameObject newFood = Instantiate(foodToSpawn, spawnPos, Quaternion.identity);
        activeFoods.Add(newFood);
    }

    bool PositionBlocked(Vector3 pos)
    {
        foreach (var playerSegment in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector3.Distance(playerSegment.transform.position, pos) < 0.1f)
                return true;
        }

        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            if (Vector3.Distance(obstacle.transform.position, pos) < 0.1f)
                return true;
        }

        return false;
    }

    public void ClearFoodList()
    {
        activeFoods.Clear();
    }
}
