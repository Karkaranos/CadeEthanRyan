/*****************************************************************************
// File Name :         LargeTumbleFiendBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 3, 2023
//
// Brief Description : Handles attacks and death conditions for Large TumbleFiends
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeTumbleFiendBehavior : MonoBehaviour
{
    #region Variables
    //TumbleFiend Specific variables
    [SerializeField] private int smallerTumblesSpawned = 3;
    [SerializeField] private GameObject smallTumble;

    //References to players and setting targets
    [SerializeField] private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    Vector3 offset;

    //General variables
    [SerializeField] private float speed = 3f;
    [SerializeField] private int cellsForDeath;
    [SerializeField] private float health = 3;
    private bool dying = false;
    #endregion Variables

    #region Functions

    /// <summary>
    /// Start is called before the first frame
    /// Sets enemy target and initial values for a couple variables
    /// </summary>
    void Start()
    {
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        player2 = GameObject.Find("Grayboxed Bandit(Clone)");
        target = Random.Range(1, 3);
        if (target == 1)
        {
            if (player1 != null)
            {
                targetObject = player1;
                print("target found");
            }
            else
            {
                targetObject = player2;
            }
        }
        else
        {
            if (player2 != null)
            {
                targetObject = player2;
                print("target found");
            }
            else
            {
                targetObject = player1;
            }
        }
        offset.x = 3;
        offset.y = 3;
        offset.y = 3;

    }



    /// <summary>
    /// Update is called once per frame
    /// Tracks the player
    /// </summary>
    void Update()
    {
        if (targetObject == null)
        {
            print("target lost");
            if (player1 != null)
            {
                targetObject = player1;
            }
            else if (player2!=null)
            {
                targetObject = player2;
            }
            
        }
        if (targetObject != null)
        {
            TrackTargetPlayer(targetObject);
        }
    }

    /// <summary>
    /// Moves towards the chosen target
    /// </summary>
    /// <param name="target">The player the enemy is targeting</param>
    void TrackTargetPlayer(GameObject target)
    {
        Vector2 targetPos = target.transform.position;
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;

        difference.x = targetPos.x - transform.position.x;
        difference.y = targetPos.y - transform.position.y;
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
    /// Checks for collisions with triggers
    /// </summary>
    /// <param name="collision"> the object collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
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
            else if (collision.name.Contains("Shotgun"))
            {
                ShotgunBulletBehavior shotbb =
                    collision.gameObject.GetComponent<ShotgunBulletBehavior>();
                health -= shotbb.damageDealt;
            }
            if (health <= 0&&!dying)
            {
                dying = true;
                OnDeath();
            }
        }
        if (collision.gameObject.tag == "explodey")
        {
            print("Tumble explode");
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
            if (health <= 0&&!dying)
            {
                dying = true;
                OnDeath();
            }
        }

    }

    /// <summary>
    /// Spawns smaller TumbleFiends on death
    /// </summary>
    public virtual void OnDeath()
    {
        GameController gc = GameObject.Find("Game Controller").
            GetComponent<GameController>();
        Vector2 spawnPos = transform.position;
        for (int i = -1; i < smallerTumblesSpawned - 1; i++)
        {
            spawnPos.x += Random.Range(-1, 1);
            spawnPos.y += Random.Range(-1, 1);
            Instantiate(smallTumble, spawnPos, transform.rotation);
            spawnPos = transform.position;
            gc.enemyCounter++;
        }
        LootTableAndDropBehavior loot = GameObject.Find("Game Controller").
            GetComponent<LootTableAndDropBehavior>();
        loot.DropLoot(transform.position);
        gc.RemoveEnemy();
        Destroy(gameObject);
    }


    #endregion Functions
}
