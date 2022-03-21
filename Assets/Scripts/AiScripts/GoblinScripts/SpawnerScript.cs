using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int EnemyPerWave = 1;
    


    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

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
