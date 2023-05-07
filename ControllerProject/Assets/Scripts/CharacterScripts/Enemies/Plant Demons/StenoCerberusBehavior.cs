/*****************************************************************************
// File Name :         StenoCerberusBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 8, 2023
//
// Brief Description : Handles attacks and death conditions for StenoCerberus, a 
                        turret-like enemy that explodes on death
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StenoCerberusBehavior : MonoBehaviour
{
    #region Variables

    //Variables for attacking
    [SerializeField] private float rateFired = .5f;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject atkPoint1;
    [SerializeField] private GameObject atkPoint2;
    [SerializeField] private GameObject atkPoint3;
    private int attackingHead;
    List<GameObject> spikesShot = new List<GameObject>();
    private int ignitionToExplode = 5;
    private int range = 15;
    private bool explodeStarted = false;
    private int healthWhileExplode = 5;
    Coroutine exploding;
    private bool killed = false;
    public float spikeDamage=1;
    public float explodeDamage=3;

    //References to players and setting targets
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    private int targetSwitchTimer;
    Vector2 targetPos;
    Vector2 distance;

    //General Variables
    [SerializeField] private float health;

    #endregion Variables

    #region Functions

    //Handles events that occur when enemy spawns
    #region Set Up
    /// <summary>
    /// Called before the first frame update; sets initial target and starts attack
    /// </summary>
    void Start()
    {
        target = 1;
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        player2 = GameObject.Find("Grayboxed Bandit(Clone)");
        targetObject = player1;
        StartCoroutine(FireSpikes());
        StartCoroutine(SwitchTarget());
    }

    #endregion

    //Handles enemy attacking and target switching
    #region Attacks

    /// <summary>
    /// Creates a turret-like behavior and spawns a spike per a specified time
    /// </summary>
    /// <returns>How often spikes spawn</returns>
    IEnumerator FireSpikes()
    {
        GameObject objectSpawned;
        for (; ; )
        {
            //Checks if the target is null. If it is, switch to the other target. 
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
                    target = 2;
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
                    target = 1;
                }
            }

            //Get the target's position and find the difference between them
            targetPos = targetObject.transform.position;
            distance.x = transform.position.x - targetPos.x;
            distance.y = transform.position.y - targetPos.y;
            distance.x = Mathf.Abs(distance.x);
            distance.y = Mathf.Abs(distance.y);

            //If the target is within range, attack
            if (distance.magnitude <= range)
            {
                //Choose a random head to attack from
                attackingHead = Random.Range(1, 4);

                //If head 1 attacks and enemy is not exploding
                if (attackingHead == 1&&!explodeStarted)
                {
                    //Spawn a spike. Add it to a list and set its target and damage
                    objectSpawned = (Instantiate(spike, atkPoint1.transform.position
                        , Quaternion.identity));
                    spikesShot.Add(objectSpawned);
                    objectSpawned.GetComponent<CactusSpikeBehavior>().
                        GetTarget(targetObject);
                    objectSpawned.GetComponent<CactusSpikeBehavior>().damageDealt = 
                        spikeDamage;
                }
                //If head 2 attacks and enemy is not exploding
                else if (attackingHead == 2&&!explodeStarted)
                {
                    //Spawn a spike. Add it to a list and set its target and damage
                    objectSpawned = (Instantiate(spike, atkPoint2.transform.position
                        , Quaternion.identity));
                    spikesShot.Add(objectSpawned);
                    objectSpawned.GetComponent<CactusSpikeBehavior>().
                        GetTarget(targetObject);
                    objectSpawned.GetComponent<CactusSpikeBehavior>().damageDealt = 
                        spikeDamage;
                }
                //If head 3 attacks and enemy is not exploding
                else if (attackingHead==3&&!explodeStarted)
                {
                    //Spawn a spike. Add it to a list and set its target and damage
                    objectSpawned = (Instantiate(spike, atkPoint3.transform.position
                        , Quaternion.identity));
                    spikesShot.Add(objectSpawned);
                    objectSpawned.GetComponent<CactusSpikeBehavior>().
                        GetTarget(targetObject);
                    objectSpawned.GetComponent<CactusSpikeBehavior>().damageDealt = 
                        spikeDamage;
                }
            }
            yield return new WaitForSeconds(rateFired);
        }
    }

    /// <summary>
    /// Switches the active target of the Cactus
    /// </summary>
    /// <returns>How often the cactus changes target</returns>
    IEnumerator SwitchTarget()
    {
        for(; ; )
        {
            //If its current target is 1 and Player 1 exists, target Player 1
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
                target = 2;
            }
            //If its current target is 2 and Player 2 exists, target Player 2
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
                target = 1;
            }

            //Set a time to randomly wait for, then wait for it
            targetSwitchTimer = Random.Range(2, 10);
            yield return new WaitForSeconds(targetSwitchTimer);
        }
    }

    #endregion

    //Handles collisions and enemy death
    #region Collisions and Death
    /// <summary>
    /// Checks if the Cactus has been hit by a bullet. 
    /// Upon Death, starts the Cactus's explosion and destroys all existing spikes
    /// </summary>
    /// <param name="collision">The object collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get a reference to the explosion script and the game controller
        FlashScript explode = GetComponent<FlashScript>();
        GameController gc = GameObject.Find("Game Controller").
            GetComponent<GameController>();

        //If hit by a bullet
        if (collision.gameObject.tag == "bullet")
        {
            //If hit by a pistol bullet, take pistol damage
            if (collision.name.Contains("Pistol"))
            {
                PistolBulletBehavior pbb =
                    collision.gameObject.GetComponent<PistolBulletBehavior>();
                health -= pbb.damageDealt;

            }

            //if hit by a revolver bullet, take revolver damage
            if (collision.name.Contains("Revolver"))
            {
                SheriffBulletBehavior sbb =
                    collision.gameObject.GetComponent<SheriffBulletBehavior>();
                health -= sbb.damageDealt;
            }

            //if hit by a shotgun/shotgun spray bullet, take shotgun damage
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

            //If health is less than 0 
            if (health <= 0)
            {
                //If not exploding, start exploding
                if (!explodeStarted)
                {
                    explode.Flash();
                    exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
                    explode.damageDealt = explodeDamage;
                    explodeStarted = true;
                    explode.spawnedBy = this.gameObject;
                }
                //Otherwise, reduce it's health while exploding
                else
                {
                    healthWhileExplode--;
                    //If its health while exploding is 0 or less, kill it
                    if (healthWhileExplode <= 0 && exploding != null && !killed)
                    {
                        StopCoroutine(exploding);
                        gc.RemoveEnemy();
                        LootTableAndDropBehavior loot = GameObject.Find
                            ("Game Controller"). GetComponent
                            <LootTableAndDropBehavior>();
                        loot.DropLoot(transform.position);
                        killed = true;
                        GameObject destroyMe;

                        //Destroy any remaining shot spikes
                        foreach (GameObject c in spikesShot)
                        {
                            destroyMe = c;
                            Destroy(destroyMe);
                        }
                        Destroy(gameObject);
                    }
                }
            }
        }

        //if hit by a player explosion
        if (collision.gameObject.tag == "explodey")
        {
            //If hit by a molotov, take molotov damage
            if (collision.name.Contains("Fire"))
            {
                FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();
                health -= fb.damageDealt;
            }

            //If hit by a firecracker or dynamite, take the corresponding damage
            else if (collision.name.Contains("Kaboom"))
            {
                DamageStoreExplodeBehavior dseb = collision.gameObject.
                    GetComponent<DamageStoreExplodeBehavior>();
                health -= dseb.damageDealt;
            }

            //if health is 0 or less
            if (health <= 0)
            {
                //If not exploding, start exploding
                if (!explodeStarted)
                {
                    explode.Flash();
                    exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
                    explodeStarted = true;
                    explode.spawnedBy = this.gameObject;
                }
                else
                {
                    healthWhileExplode--;
                    //If its health while exploding is 0 or less, kill it
                    if (healthWhileExplode <= 0 && exploding != null && !killed)
                    {
                        StopCoroutine(exploding);
                        gc.RemoveEnemy();
                        LootTableAndDropBehavior loot = GameObject.Find
                            ("Game Controller").
                            GetComponent<LootTableAndDropBehavior>();
                        loot.DropLoot(transform.position);
                        killed = true;
                        GameObject destroyMe;

                        //Remove all lingering spikes
                        foreach (GameObject c in spikesShot)
                        {
                            destroyMe = c;
                            Destroy(destroyMe);
                        }
                        Destroy(gameObject);
                    }
                }

            }
        }
    }
    #endregion

    #endregion Functions
}
