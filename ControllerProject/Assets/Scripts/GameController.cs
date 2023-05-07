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
    [SerializeField] GameObject boss;
    [SerializeField] GameObject spawnTest;
    GameObject test;
    public int enemyCounter;
    public int wave = 1;
    private int enemySpawnNum;
    private bool wavePause = false;
    private bool canSpawn = true;
    private bool gameStarted=false;
    private int numToSpawn;
    private int spawnedAtOnce=6;
    private Vector2 spawnPos;

    //Player References
    GameObject player1Obj;
    SheriffBehavior player1;
    GameObject player2Obj;
    BanditBehavior player2;


    //Enemy Spawning
    [Header("Enemy Spawn Numbers")]
    [Range(0, 10)]
    [SerializeField] private int wave1Enemies;
    [Range(0, 10)]
    [SerializeField] private int wave2Enemies;
    [Range(0, 20)]
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

    //Handles finding players and starting the game
    #region Set Up
    /// <summary>
    /// Start is called before the first frame update. Sets Wave 1 to spawn
    /// </summary>
    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }


    /// <summary>
    /// This Coroutine will repeat and check if Player 1 is null, then find Player 1
    /// and set a reference. Once it finds Player 1, it will start the game.
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckForPlayers()
    {
        for (; ; )
        {
            //If the game object for Player 1 is null, find it
            if (player1Obj == null)
            {
                player1Obj = GameObject.Find("Grayboxed Sheriff(Clone)");
            }

            //If the game object for Player 2 is null, find it
            if (player2Obj == null)
            {
                player2Obj = GameObject.Find("Grayboxed Bandit(Clone)");
            }

            //If Player 1 has been found and the game hasn't started, start it and
            //end this check
            if (player1Obj != null&&!gameStarted)
            {
                player1 = player1Obj.GetComponent<SheriffBehavior>();
                gameStarted = true;
                StartCoroutine(WaveBreak());
                StopCoroutine(CheckForPlayers());
            }
            yield return new WaitForSeconds(1);
        }
    }

    #endregion Set Up

    //Handles events that occur every frame or when enemy number changes
    #region Updates and Counters

    /// <summary>
    /// Occurs every frame. Checks if there are no enemies and calls wave spawning
    /// </summary>
    void Update()
    {
        //If both players have been destroyed and game started, go to lose screen
        if(player1Obj==null && player2Obj == null && gameStarted)
        {
            SceneManager.LoadScene("LoseScreen");
        }

        //Checks for Player 2 if player 2 is null
        if (player2Obj == null)
        {
            player2Obj = GameObject.Find("Grayboxed Bandit(Clone)");
        }

        //Sets Player 2 script if Player 2 is found
        if (player2Obj != null)
        {
            player2 = player2Obj.GetComponent<BanditBehavior>();
        }

        //If there is an active player, start these checks
        if (player1Obj != null || player2Obj!=null)
        {
            //Check if can spawn enemies
            if (enemyCounter <= 2 && enemySpawnNum < numToSpawn)
            {
                //Wait to spawn more
            }
            else
            {
                //If wave is complete and at least one player, start the break
                if (enemyCounter == 0 && wave != 9 && !wavePause && (player1Obj != 
                    null||player2Obj!=null))
                {
                    print("dsghjgd");
                    StartCoroutine(WaveBreak());
                    wave++;
                }
            }

            //If Player 1 exists and Player 2 does not, and if Player 1 is dead,
            //lose
            if (player1Obj != null && player1.Playerhealth <= 0 && player2Obj == 
                null)
            {
                SceneManager.LoadScene("LoseScreen");
            }

            //If Player 2 exists and Player 1 does not, and if Player 2 is dead, 
            //lose
            if (player2Obj != null && player2.Playerhealth <= 0 && player1Obj ==
                null)
            {
                SceneManager.LoadScene("LoseScreen");
            }
            /*if(player1Obj!=null && player2Obj!=null && player1.Playerhealth<=0 && 
                player2.Playerhealth <= 0)
            {
                SceneManager.LoadScene("LoseScreen");
            }*/
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

    #endregion

    //Handles wave spawning, Wave Breaks, and setting enemy counters
    #region Wave Manager

    /// <summary>
    /// Pause between waves. Calls wave spawning.
    /// </summary>
    /// <returns>Time between waves</returns>
    IEnumerator WaveBreak()
    {
        if (enemyCounter <= 0)
        {
            //Resets counters and starts pause
            enemyCounter = 0;
            wavePause = true;
            yield return new WaitForSeconds(3f);
            enemySpawnNum = 0;

            //If The current wave is 1, set number of enemies and start spawning
            if (wave == 1)
            {
                yield return new WaitForSeconds(5f);
                numToSpawn = wave1Enemies;
                canSpawn = true;
                StartCoroutine(Wave1Spawn());
            }

            //If the current wave is 2, set number of enemies and start spawning
            if (wave == 2)
            {
                numToSpawn = wave2Enemies;
                canSpawn = true;
                StartCoroutine(Wave2Spawn());
            }

            //If the current wave is 3, set number of enemies and start spawning
            if (wave == 3)
            {
                numToSpawn = wave3Enemies;
                canSpawn = true;
                StartCoroutine(Wave3Spawn());
            }

            //If the current wave is 4, set number of enemies and start spawning
            if (wave == 4)
            {
                numToSpawn = wave4Enemies;
                canSpawn = true;
                StartCoroutine(StaggeredSpawningWaves45678());
            }

            //If the current wave is 5, set number of enemies and allow spawning
            if (wave == 5)
            {
                numToSpawn = wave5Enemies;
                canSpawn = true;
            }

            //If the current wave is 5, set number of enemies and allow spawning
            if (wave == 6)
            {
                numToSpawn = wave6Enemies;
                canSpawn = true;
            }

            //If the current wave is 5, set number of enemies and allow spawning
            if (wave == 7)
            {
                numToSpawn = wave7Enemies;
                canSpawn = true;
            }

            //If the current wave is 5, set number of enemies and allow spawning
            if (wave == 8)
            {
                numToSpawn = wave8Enemies;
                canSpawn = true;
            }

            //If the current wave is 5, set number of enemies and allow spawning
            if (wave == 9)
            {
                numToSpawn = 1;
                canSpawn = true;
            }
            wavePause = false;
        }
    }


    /// <summary>
    /// Spawns a wave of all large tumbles for the first wave
    /// </summary>
    IEnumerator Wave1Spawn()
    {
        for(int i=0; i < numToSpawn;)
        {
            //Checks if enemies can spawn
            if (canSpawn&&wave==1)
            {
                //Check to make sure enemies don't spawn in buildings
                spawnPos = new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10));
                test = Instantiate(spawnTest, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.02f);
                print(test.GetComponent<SpawnCheckBehavior>().isOverlapping);

                //If the test is not in a building, spawn an enemy
                if (test.GetComponent<SpawnCheckBehavior>().isOverlapping == false)
                {
                    Instantiate(largeTumble, spawnPos, Quaternion.identity);
                    i++;
                    AddEnemy();
                }
                Destroy(test);

                //If all enemies have been spawned, disable spawning
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
    IEnumerator Wave2Spawn()
    {
        for (int i = 0; i < numToSpawn;)
        {
            //Checks if enemies can spawn
            if (canSpawn && wave == 2)
            {
                //Checks to make sure enemies don't spawn in buildings
                spawnPos = new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10));
                test = Instantiate(spawnTest, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.02f);
                print(test.GetComponent<SpawnCheckBehavior>().isOverlapping);

                //If the test is not in a building, spawn an enemy
                if (test.GetComponent<SpawnCheckBehavior>().isOverlapping == false)
                {
                    Instantiate(kamicactus, spawnPos, Quaternion.identity);
                    i++;
                    AddEnemy();
                }
                Destroy(test);

                //Once all enemies have been spawned, disable spawning
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
    IEnumerator Wave3Spawn()
    {
        for (int i = 0; i < numToSpawn;)
        {
            //Checks if enemies can spawn
            if (canSpawn && wave == 3)
            {
                //Checks to make sure enemies do not spawn in building
                spawnPos = new Vector2(Random.Range(-20, 20),
                    Random.Range(-20, 10));
                test = Instantiate(spawnTest, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.02f);
                print(test.GetComponent<SpawnCheckBehavior>().isOverlapping);

                //If the test spawn is not in a building, spawn an enemy
                if (test.GetComponent<SpawnCheckBehavior>().isOverlapping == false)
                {
                    Instantiate(stenocerberus, spawnPos, Quaternion.identity);
                    i++;
                    AddEnemy();
                }
                Destroy(test);

                //Once all enemies have been spawned, disable spawning
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
                        //Checks to make sure enemies do not spawn in building
                        spawnPos = new Vector2(Random.Range(-33, 40),
                            Random.Range(-25, 12));
                        test = Instantiate(spawnTest, spawnPos, Quaternion.
                            identity);
                        yield return new WaitForSeconds(.02f);
                        print(test.GetComponent<SpawnCheckBehavior>().
                            isOverlapping);

                        //If test spawn is not in building, spawn a random enemy
                        if (test.GetComponent<SpawnCheckBehavior>().isOverlapping ==
                            false)
                        {
                            //Sets a random enemy type to spawn
                            enemyType = Random.Range(1, 3);
                            print("Type Spawned: " + enemyType);

                            //If enemy type is 1, spawn a Kamicactus
                            if (enemyType == 1)
                            {
                                Instantiate(kamicactus, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }
                            else
                            {
                                //Else, spawn an Large Tumblefiend
                                Instantiate(largeTumble, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }
                        }
                        Destroy(test);

                        //If all enemies spawned, disable spawning
                        if (enemySpawnNum == numToSpawn)
                        {
                            canSpawn = false;
                        }
                    }

                    //Spawns a wave of Stenos and Tumbles for wave 5
                    if (wave == 5)
                    {
                        //Checks to make sure enemies don't spawn in buildings
                        spawnPos = new Vector2(Random.Range(-33, 40),
                            Random.Range(-25, 12));
                        test = Instantiate(spawnTest, spawnPos, Quaternion.
                            identity);
                        yield return new WaitForSeconds(.02f);

                        //If enemy won't spawn in a building
                        if (test.GetComponent<SpawnCheckBehavior>().isOverlapping ==
                            false)
                        {
                            //Sets a random enemy type
                            enemyType = Random.Range(1, 3);
                            print("Type Spawned: " + enemyType);

                            //If enemy type is 1, spawn a stenocerberus
                            if (enemyType == 1)
                            {
                                Instantiate(stenocerberus, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }
                            else
                            {
                                //Otherwise, spawn a Large Tumblefiend
                                Instantiate(largeTumble, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }
                        }
                        Destroy(test);

                        //If all enemies spawned, disable spawning
                        if (enemySpawnNum == numToSpawn)
                        {
                            canSpawn = false;
                        }
                    }

                    //Spawns a mix of all enemy types for waves 6, 7, and 8
                    if (wave >=6 && wave <=8)
                    {
                        //Check to make sure enemies won't spawn in buildings
                        spawnPos = new Vector2(Random.Range(-33, 40),
                            Random.Range(-25, 12));
                        test = Instantiate(spawnTest, spawnPos, Quaternion.
                            identity);
                        yield return new WaitForSeconds(.02f);

                        //If enemy won't spawn in a building
                        if (test.GetComponent<SpawnCheckBehavior>().isOverlapping ==
                            false)
                        {
                            //Set a random enemy type
                            enemyType = Random.Range(1, 4);
                            print("Type Spawned: " + enemyType);

                            //If enemy type is 1, spawn a Stenocerberus
                            if (enemyType == 1)
                            {
                                Instantiate(stenocerberus, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }

                            //If Enemy type is 2, spawn a Kamicactus
                            if (enemyType == 2)
                            {
                                Instantiate(kamicactus, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }

                            //If enemy type is 3, spawn a Large Tumblefiend
                            if (enemyType == 3)
                            {
                                Instantiate(largeTumble, spawnPos, 
                                    Quaternion.identity);
                                AddEnemy();
                                i++;
                            }
                        }
                        Destroy(test);

                        //If all enemies for wave spawned, disable spawning
                        if (enemySpawnNum == numToSpawn)
                        {
                            canSpawn = false;
                        }
                    }

                    if (wave == 9)
                    {
                        //Spawn the boss!
                        Instantiate(boss, new Vector2(0,0), Quaternion.identity);
                        AddEnemy();
                    }
                }
            }
            yield return new WaitForSeconds(3f);
        }
    }

    #endregion

    #endregion
}

