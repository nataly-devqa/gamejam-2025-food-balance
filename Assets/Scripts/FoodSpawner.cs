using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Массив префабов еды (заполняется через Inspector)
    public GameObject[] foodPrefabs;

    public Transform player;

    // Интервал спауна в секундах
    public float spawnInterval = 2f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFood();
            timer = 0f;
        }
    }

    void SpawnFood()
    {
        if (foodPrefabs.Length == 0 || player == null) return;

        int index = Random.Range(0, foodPrefabs.Length);

        // X — рандомно около игрока, Y — чуть выше
        float offsetX = Random.Range(-2f, 2f);
        Vector2 spawnPos = new Vector2(player.position.x + offsetX, player.position.y + 4f);

        Instantiate(foodPrefabs[index], spawnPos, Quaternion.identity);
    }

}
