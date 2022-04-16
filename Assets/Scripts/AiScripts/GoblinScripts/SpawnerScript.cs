using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    private float timeBetweenWaves = 3.5f;
    public static float countdown;
    private int EnemyPerWave = 1;
    
    void Awake()
    {
        countdown = 15f;
    }
    void Update()
    {
        countdown -= Time.deltaTime;
        //Debug.Log(countdown);
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < EnemyPerWave; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
