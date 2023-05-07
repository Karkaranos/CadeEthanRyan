/*****************************************************************************
// File Name :         BossBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Dictates basic behavior for the boss- attacks, tower spawning
                           and death
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    #region Variables

    //Attack and defense for the boss
    [SerializeField] GameObject shield;
    [SerializeField] GameObject tower;
    List<GameObject> towerList = new List<GameObject>();
    private bool canBeAttacked = false;
    GameObject activeShield;
    private bool destroyed;
    [SerializeField] GameObject pistolBullet;
    [SerializeField] GameObject shotgunBullet;
    [SerializeField] GameObject revolverBullet;
    [SerializeField] GameObject dynamite;
    [SerializeField] GameObject firecracker;
    [SerializeField] GameObject cocktail;
    private int pistolDmg=2;
    private int shotgunDmg=4;
    private int revolverDmg=1;
    private int dynamiteDmg=10;
    private int firecrackerDmg=20;
    private int cocktailDmg=15;
    private int dynamiteTimer=2;
    private int firecrackerTimer=1;
    private int cocktailTimer=3;
    private int towersToSpawn = 0;
    private bool waiting = false;
    private float exhaustionTimer = 3f;
    private bool attacking = false;
    private int attackType;
    private int targetNum;
    private int attackNum;
    [SerializeField] GameObject test;

    //Movement and positioning
    Vector3 towerSpawn;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject target;
    Vector3 targetMovePos;

    //General variables
    public float health = 300;
    private float maxHealth;
    private float speed = 3;
    private bool moveToNextPhase = true;
    [SerializeField] private int BossPhase = 5;
    GameObject temp;

    #endregion Variables

    #region Functions

    //Handles Start, Selecting a Phase, and Death behaviors
    #region Set Up
    /// <summary>
    /// Spawns an initial tower for the boss
    /// </summary>
    void Start()
    {
        //Spawn a tower to start
        maxHealth = health;
        towerSpawn = transform.position;
        towerSpawn.x -= 1;
        towerSpawn.y -= 3;
        towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));
        activeShield = Instantiate(shield, transform.position, Quaternion.identity);

        //Find both players and set an initial target
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        player2 = GameObject.Find("Grayboxed Bandit(Clone)");
        target = player1;
    }

    /// <summary>
    /// Update is called once per frame. Update handles what phase the boss is in
    /// </summary>
    void Update()
    {
        //Moves boss
        Vector3 difference = transform.position - targetMovePos;


        //If there is a shield, set it to the boss's position
        if (activeShield != null)
        {
            activeShield.transform.position = transform.position;
        }

        //if there are no towers and can move to a new phase
        if (towerList.Count == 0&&moveToNextPhase)
        {
            //If boss health is between 50% and 25%, use this weighted set
            if ((health <= (maxHealth / 2))&&(health>(maxHealth/4))) 
            {
                BossPhase = Random.Range(1, 6);
            }
            else
            {
                //If boss health is less than 25%, use this weighted set
                if (health <= maxHealth / 4)
                {
                    BossPhase = Random.Range(1, 7);
                }
                //If boss health is above 50%, use this weighted set
                else
                {
                    BossPhase = Random.Range(1, 4);
                }
            }

            moveToNextPhase = false;
            waiting = false;

            //Move phase initialization- set a position to target
            if (BossPhase == 1)
            {
                targetMovePos.x = Random.Range(-25, 32);
                targetMovePos.y = Random.Range(-25, 8);
            }

            //Tower Phase initialization- add another tower
            if (BossPhase == 5||BossPhase==6 && towersToSpawn < 4)
            {
                towersToSpawn++;
            }
        }

        //Move Phase
        if (BossPhase == 1)
        {
            BossMove();

            //If close to its target, move to next phase
            if (difference.magnitude <= 3)
            {
                moveToNextPhase = true;
            }
        }

        //Attacking Phase
        if ((BossPhase == 2 || BossPhase == 3) && !attacking)
        {
            //Set a number of attacks and start attacking
            attackNum = Random.Range(5, 10);
            BossAttack();
        }

        //Exhaustion Phase
        if (BossPhase == 4 && !waiting)
        {
            //Start being exhausted
            StartCoroutine(BossExhaustion());
        }

        //Tower Phase
        if ((BossPhase == 5||BossPhase==6)&&towerList.Count==0)
        {
            StartCoroutine(SpawnTowers());
        }
    }

    /// <summary>
    /// Removes a tower when its health reaches 0
    /// </summary>
    /// <param name="destroyedTower">The tower destroyed</param>
    public void RemoveTower(GameObject destroyedTower)
    {
        towerList.Remove(destroyedTower);
        Destroy(destroyedTower);
        print("Towers left " + towerList.Count);

        //If no towers, remove shield
        if (towerList.Count == 0)
        {
            Destroy(activeShield);
            canBeAttacked = true;
            moveToNextPhase = true;
        }
    }

    /// <summary>
    /// Occurs when boss dies; removes the last enemy and goes to win screen
    /// </summary>
    private void BossDeath()
    {
        GameController gc = GameObject.Find("Game Controller").
            GetComponent<GameController>();
        gc.RemoveEnemy();
        print("You win");

        //move to the win screen
        SceneManager.LoadScene("WinScreen");

    }
    #endregion Set Up

    //Handles actions for the boss's 4 phases
    #region Boss Phases

    /// <summary>
    /// Moves the boss towards a previously chosen random location
    /// </summary>
    private void BossMove()
    {
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;

        difference.x = targetMovePos.x - transform.position.x;
        difference.y = targetMovePos.y - transform.position.y;

        //Add force depending on the difference between the boss's position and its
        //target
        if (difference.x < 0)
        {
            moveForce.x += -1 * speed * Time.deltaTime;
        }
        else
        {
            moveForce.x += 1 * speed * Time.deltaTime;
        }
        if (difference.y < 0)
        {
            moveForce.y += -1 * speed * Time.deltaTime;
        }
        else
        {
            moveForce.y += 1 * speed * Time.deltaTime;
        }

        //Move the boss
        transform.Translate(moveForce, Space.Self);

    }

    /// <summary>
    /// Pauses the boss for a set amount of time before resuming standard behavior
    /// </summary>
    /// <returns>How long the boss pauses for</returns>
    IEnumerator BossExhaustion()
    {
        waiting = true;
        yield return new WaitForSeconds(exhaustionTimer);
        moveToNextPhase = true;
    }

    /// <summary>
    /// Selects a target and what type of attack it will be
    /// </summary>
    private void BossAttack()
    {
        //picks a target
        targetNum = Random.Range(1, 3);

        //Sets the target object if it is not null
        if (targetNum == 1)
        {
            if (player1 != null)
            {
                target = player1;
            }
            else
            {
                target = player2;
            }
        }
        else
        {
            if (player2 != null)
            {
                target = player2;
            }
            else
            {
                target = player1;
            }
        }

        //Start attacking and set an attack type
        attacking = true;
        attackType = Random.Range(1, 5);
        if (attackType == 2)
        {
            StartCoroutine(ExplosionAttack());
        }
        else
        {
            StartCoroutine(GunAttack());
        }
    }

    /// <summary>
    /// Spawns a swarm of bullets dictated by the randon number assigned. Shoots
    /// at its target
    /// </summary>
    /// <returns>Time between fires</returns>
    IEnumerator GunAttack()
    {
        //Sets a random attack type
        attackType = Random.Range(1, 4);
        for(int i=0; i<attackNum; i++)
        {
            //If attack is 1, do a revolver attack
            if (attackType == 1)
            {
                temp = Instantiate(revolverBullet, transform.position,
                    Quaternion.identity);
                temp.GetComponent<SheriffBulletBehavior>().damageDealt =
                    revolverDmg;
                temp.GetComponent<SheriffBulletBehavior>().Shoot(target);
                temp.GetComponent<SheriffBulletBehavior>().shotByPlayer = false;
            }

            //If attack is 2, do a shotgun attack
            if (attackType == 2)
            {
                temp = Instantiate(shotgunBullet, transform.position,
                    Quaternion.identity);
                temp.GetComponent<ShotgunBulletBehavior>().damageDealt =
                    shotgunDmg;
                temp.GetComponent<ShotgunBulletBehavior>().Shoot(target);
                temp.GetComponent<ShotgunBulletBehavior>().shotByPlayer = false;
            }

            //If attack is 3, do a pistol attack
            if (attackType == 3)
            {
                temp = Instantiate(pistolBullet, transform.position,
                    Quaternion.identity);
                temp.GetComponent<PistolBulletBehavior>().damageDealt =
                    pistolDmg;
                temp.GetComponent<PistolBulletBehavior>().Shoot(target);
                temp.GetComponent<PistolBulletBehavior>().shotByPlayer = false;
            }

            //wait, then attack again
            yield return new WaitForSeconds(.5f);
        }
        attacking = false;
        moveToNextPhase = true;
    }


    /// <summary>
    /// Spawns explosives directed at its target
    /// </summary>
    /// <returns> Time between fires </returns>
    IEnumerator ExplosionAttack()
    {
        //set a random attack type
        attackType = Random.Range(1, 4);

        for(int i=0; i<attackNum; i++)
        {
            //If attack is 1, spawn dynamite
            if (attackType == 1)
            {
                temp = Instantiate(dynamite, target.transform.position,
                                Quaternion.identity);
                temp.GetComponent<BanditExplodeBehavior>().bDamageDealt = 
                    dynamiteDmg;
                temp.GetComponent<BanditExplodeBehavior>().Flash();
                StartCoroutine(temp.GetComponent<BanditExplodeBehavior>().
                    Kaboom(dynamiteTimer));
            }

            //If attack is 2, spawn a molotov cocktail
            if (attackType == 2)
            {
                temp = Instantiate(cocktail, target.transform.position,
                    Quaternion.identity);
                temp.GetComponent<CocktailExplodeBehavior>().cDamageDealt = 
                    cocktailDmg;
                temp.GetComponent<CocktailExplodeBehavior>().Flash();
                StartCoroutine(temp.GetComponent<CocktailExplodeBehavior>().
                    Kaboom(cocktailTimer));
            }

            //If attack is 3, spawn a firecracker
            if (attackType == 3)
            {
                temp = Instantiate(firecracker, target.transform.position,
                    Quaternion.identity);
                temp.GetComponent<FirecrackerExplodeBehavior>().fDamageDealt =
                    firecrackerDmg;
                temp.GetComponent<FirecrackerExplodeBehavior>().Flash();
                StartCoroutine(temp.GetComponent<FirecrackerExplodeBehavior>().
                    Kaboom(firecrackerTimer));
            }

            //wait before attacking again
            yield return new WaitForSeconds(1f);
        }
        attacking = false;
        moveToNextPhase = true;
    }

    /// <summary>
    /// Spawns towers to protect the boss
    /// </summary>
    IEnumerator SpawnTowers()
    {
        for(int i=0; i<towersToSpawn;)
        {
            //Spawn a test object to make sure tower is not in building
            towerSpawn = new Vector2((transform.position.x+Random.Range(-5, 5)),
                    (transform.position.y+Random.Range(-5, 5)));
            test = Instantiate(test, towerSpawn, Quaternion.identity);
            yield return new WaitForSeconds(.02f);
            print(test.GetComponent<SpawnCheckBehavior>().isOverlapping);

            //If the test is not in a building, spawn a tower
            if (test.GetComponent<SpawnCheckBehavior>().isOverlapping == false)
            {
                towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));
                i++;
            }
            Destroy(test);

            //spawn a tower at a random position
            /*towerSpawn = transform.position;
            towerSpawn.x+=Random.Range(-5f,5f);
            towerSpawn.y += Random.Range(-5f, 5f);
            towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));*/
        }
        activeShield = Instantiate(shield, transform.position, Quaternion.identity);
    }
    #endregion Boss Phases

    //Handles collisions with player Attacks
    #region Collisions
    /// <summary>
    /// Handles collisions and triggers from player attacks
    /// </summary>
    /// <param name="collision">The attack collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the boss can be attacked
        if (canBeAttacked&&health>0)
        {
            //If attacked by a gun
            if (collision.gameObject.tag == "bullet")
            {
                //If shot by a pistol
                if (collision.name.Contains("Pistol"))
                {
                    PistolBulletBehavior pbb =
                        collision.gameObject.GetComponent<PistolBulletBehavior>();

                    //if bullet shot by a player, take damage
                    if (pbb.shotByPlayer)
                    {
                        health -= pbb.damageDealt;
                    }
                }

                //If shot by a revolver
                if (collision.name.Contains("Revolver"))
                {
                    SheriffBulletBehavior sbb =
                        collision.gameObject.GetComponent<SheriffBulletBehavior>();

                    //if bullet shot by a player, take damage
                    if (sbb.shotByPlayer)
                    {
                        health -= sbb.damageDealt;
                    }
                }

                //If shot by a shotgun or shotgun spray bullet
                if (collision.name.Contains("Spray"))
                {
                    SprayShotgunBulletBehavior ssbb =
                        collision.GetComponent<SprayShotgunBulletBehavior>();

                    //if bullet not shot by a player, take damage
                    if (ssbb.shotByPlayer)
                    {
                        health -= ssbb.damageDealt;
                    }
                }
                else if (collision.name.Contains("Shotgun"))
                {
                    ShotgunBulletBehavior shotbb =
                        collision.gameObject.GetComponent<ShotgunBulletBehavior>();

                    //if bullet not shot by a player, take damage
                    if (shotbb.shotByPlayer)
                    {
                        health -= shotbb.damageDealt;
                    }
                }

                //If health is 0 or less and not destroyed, die
                if (health <= 0 && !destroyed)
                {
                    destroyed = true;
                    BossDeath();
                }
            }

            //If attacked by an explosive
            if (collision.gameObject.tag == "explodey")
            {
                //If hit by a molotov cocktail
                if (collision.name.Contains("Fire"))
                {
                    FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();

                    //If explosion created by player, take damage
                    if (fb.shotByPlayer)
                    {
                        health -= fb.damageDealt;
                    }
                }

                //If hit by a firecracker or dynamite
                else if (collision.name.Contains("Kaboom"))
                {
                    DamageStoreExplodeBehavior dseb = collision.gameObject.
                        GetComponent<DamageStoreExplodeBehavior>();

                    //If explosion created by player, take damage
                    if (dseb.shotByPlayer)
                    {
                        health -= dseb.damageDealt;
                    }
                }

                //if health is 0 or less and not destroyed, die
                if (health <= 0 && !destroyed)
                {
                    destroyed = true;
                    BossDeath();
                }
            }
        }

    }
    #endregion Collisions

    #endregion Functions
}
