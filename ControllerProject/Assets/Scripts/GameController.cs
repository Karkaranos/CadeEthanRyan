/*****************************************************************************
// File Name :         GameController.cs
// Author :            Cade R. Naylor
// Creation Date :     April 12, 2023
//
// Brief Description : Handles scene management and spawns enemies. 
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject kamicactus;
    [SerializeField] GameObject stenocerberus;
    [SerializeField] GameObject largeTumble;
    [SerializeField] GameObject smallTumble;
    public int enemyCounter;
    private int wave = 1;
    private int enemySpawnNum;
    private bool wavePause = false;

    [SerializeField] GameObject healthBoost;
    [SerializeField] GameObject ammoBoost;
    private int secondsBeforeSpawnItem=3;

    SheriffBehavior player1;

    [Header("Enemy Spawn Numbers")]
    [Range(0, 20)]
    [SerializeField] private int wave1EnemiesSpawned;
    [Range(0, 30)]
    [SerializeField] private int wave2EnemiesSpawned;
    [Range(0, 40)]
    [SerializeField] private int wave3EnemiesSpawned;
    [Range(0, 50)]
    [SerializeField] private int wave4EnemiesSpawned;

    [Header("Enemy Spawn Chance")]
    [Range(1, 4)]
    [SerializeField] private int kamicactusSpawnMultiplier;
    [Range(1, 4)]
    [SerializeField] private int stenocerberusSpawnMultiplier;
    [Range(1, 4)]
    [SerializeField] private int largeTumbleSpawnMultiplier;
    [Range(1, 4)]
    [SerializeField] private int smallTumbleSpawnMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Grayboxed Sheriff").
            GetComponent<SheriffBehavior>();
        SpawnEnemies(wave1EnemiesSpawned);
        StartCoroutine(StatAdd());
    }

    /// <summary>
    /// Occurs every frame. Checks if there are no enemies and calls wave spawning
    /// </summary>
    void Update()
    {
        if (enemyCounter == 0 && wave != 4 && !wavePause)
        {
            StartCoroutine(WaveBreak());
            wave++;
        }

        if (player1.Playerhealth <= 0)
        {
            SceneManager.LoadScene("Lose");
        }

    }

    IEnumerator StatAdd()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(secondsBeforeSpawnItem);
            int spawnChance = Random.Range(1, 20);
            if (spawnChance == 1)
            {
                Instantiate(healthBoost, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
            }
            if (spawnChance == 2)
            {
                Instantiate(ammoBoost, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
            }
        }
    }
    IEnumerator WaveBreak()
    {
        wavePause = true;
        yield return new WaitForSeconds(3f);
        if (wave == 2)
        {
            SpawnEnemies(wave2EnemiesSpawned);
        }
        if (wave == 3)
        {
            SpawnEnemies(wave3EnemiesSpawned);
        }
        if (wave == 4)
        {
            SpawnEnemies(wave4EnemiesSpawned);
        }
        wavePause = false;

    }
    public void AddEnemy()
    {
        enemyCounter++;
    }

    public void RemoveEnemy()
    {
        enemyCounter--;
    }

    public void SpawnEnemies(int spawnMe)
    {
        int enemyType = 0;
        int enemyChance;
        for (int i = 0; i < spawnMe; i++)
        {
            enemyChance = Random.Range(1, 10);
            if (enemyChance <= 4)
            {
                enemyType = 4;
            }
            if (enemyChance > 4 && enemyChance <= 7)
            {
                enemyType = 3;
            }
            if (enemyChance == 8 || enemyChance == 9)
            {
                enemyType = 2;
            }
            if (enemyChance == 10)
            {
                enemyType = 1;
            }
            if (enemyType == kamicactusSpawnMultiplier)
            {
                Instantiate(kamicactus, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
            }
            if (enemyType == stenocerberusSpawnMultiplier)
            {
                Instantiate(stenocerberus, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
            }
            if (enemyType == largeTumbleSpawnMultiplier)
            {
                Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
            }
            if (enemyType == smallTumbleSpawnMultiplier)
            {
                Instantiate(smallTumble, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
            }
            AddEnemy();
        }
    }
}

