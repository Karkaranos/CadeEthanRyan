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
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;
    Vector3 offset;

    //General variables
    [SerializeField] private float speed = 3f;
    [SerializeField] private int cellsForDeath;
    [SerializeField] private float health = 3;
    #endregion Variables

    #region Functions

    /// <summary>
    /// Start is called before the first frame
    /// Sets enemy target and initial values for a couple variables
    /// </summary>
    void Start()
    {
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        target = 1;
        //target = Random.Range(1, 2);
        if (target == 1)
        {
            targetObject = player1;
        }
        /*else
        {
            targetObject = player2;
        }*/
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
        TrackTargetPlayer(targetObject);
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
            SheriffBulletBehavior sbb =
                collision.gameObject.GetComponent<SheriffBulletBehavior>();
            health -= sbb.damageDealt;
            if (health <= 0)
            {
                OnDeath();
            }
        }
        if (collision.gameObject.name == "Kaboom(Clone)")
        {
            BanditExplodeBehavior beb =
                collision.gameObject.GetComponent<BanditExplodeBehavior>();
            health -= beb.damageDealt;
            if (health <= 0)
            {
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
            gc.AddEnemy();
        }
        LootTableAndDropBehavior loot = GameObject.Find("Game Controller").
            GetComponent<LootTableAndDropBehavior>();
        loot.DropLoot(transform.position);
        Destroy(gameObject);
        gc.RemoveEnemy();
    }


    #endregion Functions
}
