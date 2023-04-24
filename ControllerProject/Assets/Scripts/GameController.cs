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
    #region Variables
    //Enemy references and spawning
    [SerializeField] GameObject kamicactus;
    [SerializeField] GameObject stenocerberus;
    [SerializeField] GameObject largeTumble;
    [SerializeField] GameObject smallTumble;
    public int enemyCounter;
    public int wave = 1;
    private int enemySpawnNum;
    private bool wavePause = false;
    private bool canSpawn = true;
    private bool gameStarted=false;

    //Player References
    GameObject player1Obj;
    SheriffBehavior player1;


    //Enemy Spawning
    [Header("Enemy Spawn Numbers")]
    [Range(0, 10)]
    [SerializeField] private int wave1EnemiesSpawned;
    [Range(0, 10)]
    [SerializeField] private int wave2EnemiesSpawned;
    [Range(0, 10)]
    [SerializeField] private int wave3EnemiesSpawned;
    [Range(0, 20)]
    [SerializeField] private int wave4EnemiesSpawned;
    [Range(0, 20)]
    [SerializeField] private int wave5EnemiesSpawned;
    [Range(0, 30)]
    [SerializeField] private int wave6EnemiesSpawned;
    [Range(0, 40)]
    [SerializeField] private int wave7EnemiesSpawned;
    [Range(0, 40)]
    [SerializeField] private int wave8EnemiesSpawned;


    /*[Header("Enemy Spawn Chance")]
    [Range(1, 4)]
    [SerializeField] private int kamicactusSpawnMultiplier;
    [Range(1, 4)]
    [SerializeField] private int stenocerberusSpawnMultiplier;
    [Range(1, 4)]
    [SerializeField] private int largeTumbleSpawnMultiplier;
    [Range(1, 4)]
    [SerializeField] private int smallTumbleSpawnMultiplier;*/
    #endregion

    #region Functions

    /// <summary>
    /// Start is called before the first frame update. Sets Wave 1 to spawn
    /// </summary>
    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }

    IEnumerator CheckForPlayers()
    {
        for (; ; )
        {
            if (player1Obj == null)
            {
                player1Obj = GameObject.Find("Grayboxed Sheriff(Clone)");
            }
            if (player1Obj != null&&!gameStarted)
            {
                player1 = player1Obj.GetComponent<SheriffBehavior>();
                Wave1Spawn();
                gameStarted = true;
                StopCoroutine(CheckForPlayers());
            }
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Occurs every frame. Checks if there are no enemies and calls wave spawning
    /// </summary>
    void Update()
    {
        if (player1Obj != null)
        {
            if (enemyCounter == 0 && wave != 5 && !wavePause && player1Obj != null)
            {
                StartCoroutine(WaveBreak());
                wave++;
            }

            if (player1.Playerhealth <= 0 && player1Obj != null)
            {
                SceneManager.LoadScene("Lose");
            }
        }

    }


    /// <summary>
    /// Pause between waves. Calls wave spawning.
    /// </summary>
    /// <returns>Time between waves</returns>
    IEnumerator WaveBreak()
    {
        wavePause = true;
        yield return new WaitForSeconds(3f);
        if (wave == 2)
        {
            canSpawn = true;
            Wave2Spawn();
        }
        if (wave == 3)
        {
            canSpawn = true;
            Wave3Spawn();
        }
        if (wave == 4)
        {
            canSpawn = true;
            Wave4Spawn();
        }
        if (wave == 5)
        {
            canSpawn = true;
            Wave5Spawn();
        }
        wavePause = false;

    }


    /// <summary>
    /// Adds enemies to a counter
    /// </summary>
    public void AddEnemy()
    {
        enemyCounter++;
    }
    
    /// <summary>
    /// Removes enemies from a counter
    /// </summary>
    public void RemoveEnemy()
    {
        enemyCounter--;
    }

    #region Wave Manager

    /// <summary>
    /// Spawns enemies. Enemies are spawned randomly using weighted percentages.
    /// </summary>
    /// <param name="spawnMe"></param>
    /*public void SpawnEnemies(int spawnMe)
    {
        int enemyType = 0;
        int enemyChance;
        for (int i = 0; i < spawnMe; i++)
        {
            //Set the enemy spawn type
            enemyChance = Random.Range(1, 10);
            if (enemyChance <= 6)
            {
                enemyType = 4;
            }
            if (enemyChance > 6 && enemyChance <= 9)
            {
                enemyType = 3;
            }
            /*if (enemyChance == 8 || enemyChance == 9)
            {
                enemyType = 2;
            }*//*
            if (enemyChance == 10)
            {
                enemyType = 1;
            }

            //Spawn the enemies corresponding to the type being spawned
            if (enemyType == kamicactusSpawnMultiplier&&enemyCounter<spawnMe)
            {
                Instantiate(kamicactus, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
                AddEnemy();
            }
            if (enemyType == stenocerberusSpawnMultiplier && enemyCounter < spawnMe)
            {
                Instantiate(stenocerberus, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
                AddEnemy();
            }
            if (enemyType == largeTumbleSpawnMultiplier && enemyCounter < spawnMe)
            {
                Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
                AddEnemy();
            }
            if (enemyType == smallTumbleSpawnMultiplier && enemyCounter < spawnMe)
            {
                Instantiate(smallTumble, new Vector2(Random.Range(-33, 40),
                    Random.Range(-32, 14)), Quaternion.identity);
                AddEnemy();
            }
        }
    }*/

    /// <summary>
    /// Spawns a wave of all large tumbles for the first wave
    /// </summary>
    public void Wave1Spawn()
    {
        for(int i=0; i < wave1EnemiesSpawned;i++)
        {
            if (canSpawn&&wave==1)
            {
                print("Wave 1 spawn");
                Instantiate(largeTumble, new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10)), Quaternion.identity);
                AddEnemy();
                if (enemyCounter == wave1EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave of all kamicactus for the second wave
    /// </summary>
    public void Wave2Spawn()
    {
        for (int i = 0; i < wave2EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 2)
            {
                print("Wave 2 spawn");
                Instantiate(kamicactus, new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10)), Quaternion.identity);
                AddEnemy();
                if (enemyCounter == wave2EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave of all Stenocerberus for the third wave
    /// </summary>
    public void Wave3Spawn()
    {
        for (int i = 0; i < wave3EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 3)
            {
                print("Wave 3 spawn");
                Instantiate(stenocerberus, new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10)), Quaternion.identity);
                AddEnemy();
                if (enemyCounter == wave3EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave with a mix of kamicactus and tumbles for the fourth wave
    /// </summary>
    public void Wave4Spawn()
    {
        int enemyType;
        for (int i = 0; i < wave4EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 4)
            {
                print("Wave 4 spawn");
                enemyType = Random.Range(1, 3);
                print("Type Spawned: " + enemyType);
                if (enemyType == 1)
                {
                    Instantiate(kamicactus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                else
                {
                    Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyCounter == wave4EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave with stenocerberus and tumbles for the 5th wave
    /// </summary>
    public void Wave5Spawn()
    {
        int enemyType;
        for (int i = 0; i < wave5EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 5)
            {
                print("Wave 5 spawn");
                enemyType = Random.Range(1, 3);
                print("Type Spawned: " + enemyType);
                if (enemyType == 1)
                {
                    Instantiate(stenocerberus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                else
                {
                    Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyCounter == wave5EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave with all types for the 6th wave
    /// </summary>
    public void Wave6Spawn()
    {
        int enemyType;
        for (int i = 0; i < wave6EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 6)
            {
                print("Wave 6 spawn");
                enemyType = Random.Range(1, 4);
                print("Type Spawned: " + enemyType);
                if (enemyType == 1)
                {
                    Instantiate(stenocerberus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyType == 2)
                {
                    Instantiate(kamicactus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyType == 3)
                {
                    Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyCounter == wave6EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave with all types for the 7th wave
    /// </summary>
    public void Wave7Spawn()
    {
        int enemyType;
        for (int i = 0; i < wave7EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 7)
            {
                print("Wave 7 spawn");
                enemyType = Random.Range(1, 4);
                print("Type Spawned: " + enemyType);
                if (enemyType == 1)
                {
                    Instantiate(stenocerberus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyType == 2)
                {
                    Instantiate(kamicactus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyType == 3)
                {
                    Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyCounter == wave7EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a wave with all enemy types for the 8th wave
    /// </summary>
    public void Wave8Spawn()
    {
        int enemyType;
        for (int i = 0; i < wave8EnemiesSpawned; i++)
        {
            if (canSpawn && wave == 8)
            {
                print("Wave 8 spawn");
                enemyType = Random.Range(1, 4);
                print("Type Spawned: " + enemyType);
                if (enemyType == 1)
                {
                    Instantiate(stenocerberus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyType == 2)
                {
                    Instantiate(kamicactus, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyType == 3)
                {
                    Instantiate(largeTumble, new Vector2(Random.Range(-33, 40),
                        Random.Range(-32, 14)), Quaternion.identity);
                    AddEnemy();
                }
                if (enemyCounter == wave8EnemiesSpawned)
                {
                    canSpawn = false;
                }
            }
        }
    }
    #endregion

    #endregion
}

