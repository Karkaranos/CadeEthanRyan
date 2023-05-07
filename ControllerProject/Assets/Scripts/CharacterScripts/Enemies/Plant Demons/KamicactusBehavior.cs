/*****************************************************************************
// File Name :         KamicactusBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 3, 2023
//
// Brief Description : Kamicactus Enemy Type- handles health, damage, movement, and
                        attacks
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamicactusBehavior : MonoBehaviour
{
    #region Variables

    //General variables
    [SerializeField] private float speed = 3f;
    [SerializeField] private int cellsForDeath;
    [SerializeField] private float health = 3;
    [SerializeField] private int healthWhileExplode = 10;
    Coroutine exploding;

    //References to players and setting targets
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    Vector3 offset;

    //Variables for exploding
    FlashScript explode;
    private float ignitionToExplode = 3;
    private bool explodeStarted = false;
    [SerializeField] private int explosionSize = 3;
    [SerializeField] GameObject explodeRange;
    public bool killed=false;
    public float damageDealt=3;

    #endregion

    #region Functions

    //Handles initialization
    #region Set Up

    /// <summary>
    /// Start is called before the first frame
    /// Sets enemy target and initial values for a couple variables
    /// </summary>
    void Start()
    {
        //Check for both players
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        player2 = GameObject.Find("Grayboxed Bandit(Clone)");

        //Set a random target
        target = Random.Range(1,3);

        //If target is not null, set target to assigned number. Otherwise, set 
        //target to active player
        if (target == 1)
        {
            if (player1 != null)
            {
                targetObject = player1;
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
            }
            else
            {
                targetObject = player1;
            }
        }

        //Get a reference to its explosion state
        explode = GetComponent<FlashScript>();

    }

    #endregion Set Up

    #region Movement and Tracking

    /// <summary>
    /// Update is called once per frame
    /// Tracks the player
    /// </summary>
    void FixedUpdate()
    {
        //Track the target
        TrackTargetPlayer(targetObject);

        //Check if the target object is null. If it is, switch targets
        if (targetObject == null)
        {
            if (target == 1)
            {
                if (player1 != null)
                {
                    targetObject = player1;
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
                }
                else
                {
                    targetObject = player1;
                }
            }
        }
    }

    /// <summary>
    /// Moves towards the chosen target
    /// </summary>
    /// <param name="target">The player the enemy is targeting</param>
    void TrackTargetPlayer(GameObject target)
    {
        //Get the target's position
        Vector2 targetPos = target.transform.position;
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;

        //Find the distance between the target and enemy
        difference.x = targetPos.x - transform.position.x;
        difference.y = targetPos.y - transform.position.y;

        //Set a direction to move towards the target
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

        //Move the enemy towards the target
        transform.Translate(moveForce, Space.Self);

        //Get the absolute value of the distance between the two
        difference.x = Mathf.Abs(difference.x);
        difference.y = Mathf.Abs(difference.y);

        //If the enemy is close to its target and has not started exploding, explode
        if(difference.magnitude <= 5&&!explodeStarted)
        {
            explode.Flash();
            explode.damageDealt = damageDealt;
            exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
            explodeStarted = true;
            explode.spawnedBy = gameObject;
            health = 0;
            speed = 2;
        }

    }

    #endregion

    //Handles collisions
    #region Collisions

    /// <summary>
    /// Handles enemy life loss and death due to attacks
    /// </summary>
    /// <param name="collision">The object triggered by </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If triggered by a player bullet
        if(collision.gameObject.tag == "bullet")
        {
            //If hit by a pistol bullet, take pistol damage
            if (collision.name.Contains("Pistol"))
            {
                PistolBulletBehavior pbb = 
                    collision.gameObject.GetComponent<PistolBulletBehavior>();
                health -= pbb.damageDealt;

            }

            //If hit by a revolver bullet, take revolver damage
            if (collision.name.Contains("Revolver"))
            {
                SheriffBulletBehavior sbb =
                    collision.gameObject.GetComponent<SheriffBulletBehavior>();
                health -= sbb.damageDealt;
            }

            //If hit by a shotgun/spray shotgun bullet, take shotgun damage
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


            GameController gc = GameObject.Find("Game Controller").
                GetComponent<GameController>();

            //If health is 0 or less
            if (health <= 0)
            {
                //If not exploding, explode
                if (!explodeStarted)
                {
                    explode.Flash();
                    exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
                    explodeStarted = true;
                    explode.spawnedBy = gameObject;
                    speed = 2;
                }
                else
                {
                    //if exploding, reduce health while exploding
                    healthWhileExplode--;

                    //If health while exploding is 0 or less, die
                    if (healthWhileExplode <= 0&&exploding!=null&&!killed)
                    {
                        StopCoroutine(exploding);
                        gc.RemoveEnemy();
                        LootTableAndDropBehavior loot = GameObject.Find
                            ("Game Controller").
                            GetComponent<LootTableAndDropBehavior>();
                        loot.DropLoot(transform.position);
                        killed = true;
                        Destroy(gameObject);
                    }
                }
            }
        }

        //If hit by a player explosion
        if (collision.gameObject.tag == "explodey")
        {
            GameController gc = GameObject.Find("Game Controller").
                GetComponent<GameController>();

            //if hit by molotov, take molotov damage
            if (collision.name.Contains("Fire"))
            {
                FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();
                health -= fb.damageDealt;
            }

            //if hit by firecracker or dynamite, take firecracker/dynamite damage
            else if (collision.name.Contains("Kaboom"))
            {
                DamageStoreExplodeBehavior dseb = collision.gameObject.
                    GetComponent<DamageStoreExplodeBehavior>();
                health -= dseb.damageDealt;
            }

            //if health is 0 or less
            if (health <= 0)
            {
                //if not exploding, explode
                if (!explodeStarted)
                {
                    explode.Flash();
                    exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
                    explodeStarted = true;
                    explode.spawnedBy = gameObject;
                    speed = 2;
                }
                else
                {
                    //if exploding, reduce explosion health
                    healthWhileExplode--;

                    //if explosion health is 0 or less, die
                    if (healthWhileExplode <= 0 && exploding != null&&!killed)
                    {
                        StopCoroutine(exploding);
                        gc.RemoveEnemy();
                        LootTableAndDropBehavior loot = GameObject.Find
                            ("Game Controller").
                            GetComponent<LootTableAndDropBehavior>();
                        loot.DropLoot(transform.position);
                        killed = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    #endregion

    #endregion
}
