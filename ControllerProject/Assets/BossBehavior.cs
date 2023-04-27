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
    private int towersToSpawn = 0;
    private bool waiting = false;
    private float exhaustionTimer = 3f;
    private bool attacking = false;
    private int attackType;
    private int targetNum;

    //Movement and positioning
    Vector3 towerSpawn;
    GameObject player1;
    GameObject player2;
    GameObject target;
    Vector3 targetMovePos;

    //General variables
    public float health = 200;
    private float speed = 3;
    private bool moveToNextPhase = true;
    [SerializeField] private int BossPhase = 5;

    #endregion Variables

    #region Functions


    /// <summary>
    /// Spawns an initial tower for the boss
    /// </summary>
    void Start()
    {
        towerSpawn = transform.position;
        towerSpawn.x -= 1;
        towerSpawn.y -= 3;
        towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));
        activeShield = Instantiate(shield, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Update is called once per frame. Update handles what phase the boss is in
    /// </summary>
    void Update()
    {
        Vector3 difference = transform.position - targetMovePos;
        if (towerList.Count == 0&&moveToNextPhase)
        {
            BossPhase = Random.Range(1, 5);
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

        //Tower Phase
        if (BossPhase == 2&&towerList.Count==0)
        {
            SpawnTowers();
        }

        //Exhaustion Phase
        if (BossPhase == 3 && !waiting)
        {
            StartCoroutine(BossExhaustion());
        }

        //Attacking Phase
        if (BossPhase == 4&&!attacking)
        {
            BossAttack();
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


    private void BossDeath()
    {
        GameController gc = GameObject.Find("Game Controller").
            GetComponent<GameController>();

    }

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

    private void BossAttack()
    {
        attacking = true;
        attackType = Random.Range(1, 5);
        if (attackType == 2)
        {
            ExplosionAttack();
        }
        else
        {
            GunAttack();
        }
        targetNum = Random.Range(1, 3);
        if (targetNum == 1)
        {
            target = player1;
        }
        else
        {
            target = player2;
        }
    }

    private void GunAttack()
    {
        attackType = Random.Range(1, 4);
        if (attackType == 1)
        {
            //Revolver Bullet
        }
        if (attackType == 2)
        {
            //Shotgun Bullet
        }
        if (attackType == 3)
        {
            //Pistol bullet
        }
        attacking = false;
    }

    private void ExplosionAttack()
    {
        attackType = Random.Range(1, 4);
        if (attackType == 1)
        {
            //Dynamite
        }
        if (attackType == 2)
        {
            //Cocktail
        }
        if (attackType == 3)
        {
            //Firecracker
        }
        attacking = false;
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
                }
            }
            if (collision.gameObject.tag == "explodey")
            {
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

                }
            }
        }

    }
    #endregion Collisions

    #endregion Functions
}
