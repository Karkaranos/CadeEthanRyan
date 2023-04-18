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


    //Items
    [SerializeField] GameObject healthBoost;
    [SerializeField] GameObject ammoBoost;
    private int secondsBeforeSpawnItem=3;

    //Player References
    GameObject player1Obj;
    SheriffBehavior player1;


    //Enemy Spawning
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
            if (player1Obj != null)
            {
                player1 = player1Obj.GetComponent<SheriffBehavior>();
                SpawnEnemies(wave1EnemiesSpawned);
                StartCoroutine(StatAdd());
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
            if (enemyCounter == 0 && wave != 4 && !wavePause && player1Obj != null)
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
    /// Spawn items to increase the player's stats
    /// </summary>
    /// <returns>How long between potential spawns</returns>
    IEnumerator StatAdd()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(secondsBeforeSpawnItem);
            int spawnChance = Random.Range(1, 16);
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


    /// <summary>
    /// Spawns enemies. Enemies are spawned randomly using weighted percentages.
    /// </summary>
    /// <param name="spawnMe"></param>
    public void SpawnEnemies(int spawnMe)
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
            }*/
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
    }

    #endregion
}

