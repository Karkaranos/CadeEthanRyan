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
        maxHealth = health;
        towerSpawn = transform.position;
        towerSpawn.x -= 1;
        towerSpawn.y -= 3;
        towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));
        activeShield = Instantiate(shield, transform.position, Quaternion.identity);
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        player2 = GameObject.Find("Grayboxed Bandit(Clone)");
        target = player1;
    }

    /// <summary>
    /// Update is called once per frame. Update handles what phase the boss is in
    /// </summary>
    void Update()
    {
        if (activeShield != null)
        {
            activeShield.transform.position = transform.position;
        }
        Vector3 difference = transform.position - targetMovePos;
        if (towerList.Count == 0&&moveToNextPhase)
        {
            //WEIGH THIS

            if (health <= (maxHealth / 2))
            {
                BossPhase = Random.Range(1, 6);
            }
            else
            {
                BossPhase = Random.Range(1, 4);
            }
            moveToNextPhase = false;
            waiting = false;
            //Move phase initialization
            if (BossPhase == 1)
            {
                targetMovePos.x = Random.Range(-25, 32);
                targetMovePos.y = Random.Range(-25, 8);
            }
            //Tower Phase initialization
            if (BossPhase == 2 && towersToSpawn < 4)
            {
                towersToSpawn++;
            }
        }

        //Move Phase
        if (BossPhase == 1)
        {
            BossMove();

            if (difference.magnitude <= 3)
            {
                moveToNextPhase = true;
            }
        }

        //Attacking Phase
        if ((BossPhase == 2 || BossPhase == 3) && !attacking)
        {
            attackNum = Random.Range(1, 6);
            BossAttack();
        }

        //Tower Phase
        if (BossPhase == 4&&towerList.Count==0)
        {
            SpawnTowers();
        }

        //Exhaustion Phase
        if (BossPhase == 5 && !waiting)
        {
            StartCoroutine(BossExhaustion());
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
        targetNum = Random.Range(1, 3);
        if (targetNum == 1)
        {
            target = player1;
            print("Target 1");
        }
        else
        {
            if (player2 != null)
            {
                target = player2;
                print("Target 2");
            }
            else
            {
                target = player1;
                print("Target Switched to 1");
            }
        }
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
        attackType = Random.Range(1, 4);
        for(int i=0; i<attackNum; i++)
        {
            if (attackType == 1)
            {
                temp = Instantiate(revolverBullet, transform.position,
                    Quaternion.identity);
                temp.GetComponent<SheriffBulletBehavior>().damageDealt =
                    revolverDmg;
                temp.GetComponent<SheriffBulletBehavior>().Shoot(target);
                temp.GetComponent<SheriffBulletBehavior>().shotByPlayer = false;
            }
            if (attackType == 2)
            {
                temp = Instantiate(shotgunBullet, transform.position,
                    Quaternion.identity);
                temp.GetComponent<ShotgunBulletBehavior>().damageDealt =
                    shotgunDmg;
                temp.GetComponent<ShotgunBulletBehavior>().Shoot(target);
                temp.GetComponent<ShotgunBulletBehavior>().shotByPlayer = false;
            }
            if (attackType == 3)
            {
                temp = Instantiate(pistolBullet, transform.position,
                    Quaternion.identity);
                temp.GetComponent<PistolBulletBehavior>().damageDealt =
                    pistolDmg;
                temp.GetComponent<PistolBulletBehavior>().Shoot(target);
                temp.GetComponent<PistolBulletBehavior>().shotByPlayer = false;
            }
            yield return new WaitForSeconds(.5f);
        }
        attacking = false;
        moveToNextPhase = true;
    }

    IEnumerator ExplosionAttack()
    {
        attackType = Random.Range(1, 4);
        for(int i=0; i<attackNum; i++)
        {
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
            yield return new WaitForSeconds(1f);
        }
        attacking = false;
        moveToNextPhase = true;
    }

    /// <summary>
    /// Spawns towers to protect the boss
    /// </summary>
    private void SpawnTowers()
    {
        for(int i=0; i<towersToSpawn; i++)
        {
            towerSpawn = transform.position;
            towerSpawn.x+=Random.Range(-5f,5f);
            towerSpawn.y += Random.Range(-5f, 5f);
            towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));
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
        if (canBeAttacked&&health>0)
        {
            if (collision.gameObject.tag == "bullet")
            {
                if (collision.name.Contains("Pistol"))
                {
                    PistolBulletBehavior pbb =
                        collision.gameObject.GetComponent<PistolBulletBehavior>();

                    //if bullet not shot by a player, take damage
                    if (pbb.shotByPlayer)
                    {
                        health -= pbb.damageDealt;
                    }
                }
                if (collision.name.Contains("Revolver"))
                {
                    SheriffBulletBehavior sbb =
                        collision.gameObject.GetComponent<SheriffBulletBehavior>();

                    //if bullet not shot by a player, take damage
                    if (sbb.shotByPlayer)
                    {
                        health -= sbb.damageDealt;
                    }
                }
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
                if (health <= 0 && !destroyed)
                {
                    destroyed = true;
                    BossDeath();
                }
            }
            if (collision.gameObject.tag == "explodey")
            {
                if (collision.name.Contains("Fire"))
                {
                    FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();

                    //If explosion not created by player, take damage
                    if (fb.shotByPlayer)
                    {
                        health -= fb.damageDealt;
                    }
                }
                else if (collision.name.Contains("Kaboom"))
                {
                    DamageStoreExplodeBehavior dseb = collision.gameObject.
                        GetComponent<DamageStoreExplodeBehavior>();

                    //If explosion not created by player, take damage
                    if (dseb.shotByPlayer)
                    {
                        health -= dseb.damageDealt;
                    }
                }
                if (health <= 0 && !destroyed)
                {
                    destroyed = true;
                    BossDeath();
                }
            }

            /*if (collision.gameObject.tag == "bullet")
            {
                if (collision.name.Contains("Pistol"))
                {
                    PistolBulletBehavior pbb =
                        collision.gameObject.GetComponent<PistolBulletBehavior>();
                    health -= pbb.damageDealt;

                }
                if (collision.name.Contains("Revolver"))
                {
                    SheriffBulletBehavior sbb =
                        collision.gameObject.GetComponent<SheriffBulletBehavior>();
                    health -= sbb.damageDealt;
                }
                if (collision.name.Contains("Spray"))
                {
                    SprayShotgunBulletBehavior ssbb =
                        collision.GetComponent<SprayShotgunBulletBehavior>();
                    health -= ssbb.damageDealt;
                }
                if (collision.name.Contains("Shotgun"))
                {
                    ShotgunBulletBehavior shotbb =
                        collision.gameObject.GetComponent<ShotgunBulletBehavior>();
                    health -= shotbb.damageDealt;
                }
                if (health <= 0 && !destroyed)
                {
                    destroyed = true;
                    BossDeath();
                }
            }
            if (collision.gameObject.tag == "explodey")
            {
                print("explode hit");
                if (collision.name.Contains("Fire"))
                {
                    FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();
                    health -= fb.damageDealt;
                }
                else if (collision.name.Contains("Kaboom"))
                {
                    DamageStoreExplodeBehavior dseb = collision.gameObject.
                        GetComponent<DamageStoreExplodeBehavior>();
                    health -= dseb.damageDealt;
                }
                if (health <= 0 && !destroyed)
                {
                    destroyed = true;
                    BossDeath();

                }
            }*/
        }

    }
    #endregion Collisions

    #endregion Functions
}
