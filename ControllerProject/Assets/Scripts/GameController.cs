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
    private int numToSpawn;
    private int spawnedAtOnce=6;


    private int changeKamiRate;
    private int changeTumbleRate;
    private int changeStenoRate;

    //Player References
    GameObject player1Obj;
    SheriffBehavior player1;


    //Enemy Spawning
    [Header("Enemy Spawn Numbers")]
    [Range(0, 10)]
    [SerializeField] private int wave1Enemies;
    [Range(0, 10)]
    [SerializeField] private int wave2Enemies;
    [Range(0, 10)]
    [SerializeField] private int wave3Enemies;
    [Range(0, 20)]
    [SerializeField] private int wave4Enemies;
    [Range(0, 20)]
    [SerializeField] private int wave5Enemies;
    [Range(0, 30)]
    [SerializeField] private int wave6Enemies;
    [Range(0, 40)]
    [SerializeField] private int wave7Enemies;
    [Range(0, 40)]
    [SerializeField] private int wave8Enemies;


    [Header("Enemy Spawn Chance")]
    [Range(1, 3)]
    [SerializeField] private int kamicactusSpawnMultiplier;
    [Range(1, 3)]
    [SerializeField] private int stenocerberusSpawnMultiplier;
    [Range(1, 3)]
    [SerializeField] private int largeTumbleSpawnMultiplier;
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
                gameStarted = true;
                numToSpawn = wave1Enemies;
                Wave1Spawn();
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
            if (enemyCounter <= 2 && enemySpawnNum < numToSpawn)
            {
                print("Spawn More");
            }
            else
            {
                if (enemyCounter == 0 && wave != 9 && !wavePause && player1Obj != null)
                {
                    print("dsghjgd");
                    StartCoroutine(WaveBreak());
                    wave++;
                }
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
        if (enemyCounter <= 0)
        {
            enemyCounter = 0;
            wavePause = true;
            yield return new WaitForSeconds(3f);
            enemySpawnNum = 0;
            if (wave == 2)
            {
                numToSpawn = wave2Enemies;
                canSpawn = true;
                Wave2Spawn();
            }
            if (wave == 3)
            {
                numToSpawn = wave3Enemies;
                canSpawn = true;
                Wave3Spawn();
            }
            if (wave == 4)
            {
                numToSpawn = wave4Enemies;
                canSpawn = true;
                StartCoroutine(StaggeredSpawningWaves45678());
            }
            if (wave == 5)
            {
                numToSpawn = wave5Enemies;
                canSpawn = true;
            }
            if (wave == 6)
            {
                numToSpawn = wave6Enemies;
                canSpawn = true;
            }
            if (wave == 7)
            {
                numToSpawn = wave7Enemies;
                canSpawn = true;
            }
            if (wave == 8)
            {
                numToSpawn = wave8Enemies;
                canSpawn = true;
            }
            wavePause = false;
        }
    }


    /// <summary>
    /// Adds enemies to a counter
    /// </summary>
    public void AddEnemy()
    {
        enemyCounter++;
        enemySpawnNum++;
        print(enemySpawnNum);
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
    /// Spawns a wave of all large tumbles for the first wave
    /// </summary>
    public void Wave1Spawn()
    {
        for(int i=0; i < numToSpawn;i++)
        {
            if (canSpawn&&wave==1)
            {
                print("Wave 1 spawn");
                Instantiate(largeTumble, new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10)), Quaternion.identity);
                AddEnemy();
                if (enemySpawnNum == numToSpawn)
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
        for (int i = 0; i < numToSpawn; i++)
        {
            if (canSpawn && wave == 2)
            {
                print("Wave 2 spawn");
                Instantiate(kamicactus, new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10)), Quaternion.identity);
                AddEnemy();
                if (enemySpawnNum == numToSpawn)
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
        for (int i = 0; i < numToSpawn; i++)
        {
            if (canSpawn && wave == 3)
            {
                print("Wave 3 spawn");
                Instantiate(stenocerberus, new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10)), Quaternion.identity);
                AddEnemy();
                if (enemySpawnNum == numToSpawn)
                {
                    canSpawn = false;
                }
            }
        }
    }

    /// <summary>
    /// Intermittently spawns enemies for waves 4-8. Enemies are spawned when there
    /// is a limited number of alive enemies and the total enemy count for the wave
    /// has not yet been reached
    /// </summary>
    /// <returns>How often it checks to spawn enemies </returns>
    IEnumerator StaggeredSpawningWaves45678()
    {
        int enemyType;
        for(; ; )
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                if (canSpawn && enemyCounter < spawnedAtOnce && enemySpawnNum <
                    numToSpawn)
                {
                    //Spawns a mix of Kamicactus and Tumbles for wave 4
                    if (wave == 4)
                    {
                        print("Wave 4 spawn");
                        enemyType = Random.Range(1, 3);
                        print("Type Spawned: " + enemyType);
                        if (enemyType == 1)
                        {
                            Instantiate(kamicactus, new Vector2(Random.Range(-33, 
                                40),Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        else
                        {
                            Instantiate(largeTumble, new Vector2(Random.Range(-33, 
                                40),Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        if (enemySpawnNum == numToSpawn)
                        {
                            canSpawn = false;
                        }
                    }

                    //Spawns a wave of Stenos and Tumbles for wave 5
                    if (wave == 5)
                    {
                        print("Wave 5 spawn");
                        enemyType = Random.Range(1, 3);
                        print("Type Spawned: " + enemyType);
                        if (enemyType == 1)
                        {
                            Instantiate(stenocerberus, new Vector2(Random.Range(-33,
                                40),Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        else
                        {
                            Instantiate(largeTumble, new Vector2(Random.Range(-33, 
                                40), Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        if (enemySpawnNum == numToSpawn)
                        {
                            canSpawn = false;
                        }
                    }


                    //Spawns a mix of all enemy types for waves 6, 7, and 8
                    if (wave >=6 && wave <=8)
                    {
                        print("Wave " + wave +  " spawn");
                        enemyType = Random.Range(1, 4);
                        print("Type Spawned: " + enemyType);
                        if (enemyType == 1)
                        {
                            Instantiate(stenocerberus, new Vector2(Random.Range(-33,
                                40), Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        if (enemyType == 2)
                        {
                            Instantiate(kamicactus, new Vector2(Random.Range(-33, 
                                40),Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        if (enemyType == 3)
                        {
                            Instantiate(largeTumble, new Vector2(Random.Range(-33, 
                                40), Random.Range(-32, 14)), Quaternion.identity);
                            AddEnemy();
                        }
                        if (enemySpawnNum == numToSpawn)
                        {
                            canSpawn = false;
                        }
                    }

                    if (wave == 9)
                    {
                        print("Boss");
                    }
                }
            }
            yield return new WaitForSeconds(3f);
        }
    }

    #endregion

    #endregion
}

