/*****************************************************************************
// File Name :         KamicactusBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 3, 2023
//
// Brief Description : Kamicactus Enemy Type
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


    /// <summary>
    /// Start is called before the first frame
    /// Sets enemy target and initial values for a couple variables
    /// </summary>
    void Start()
    {
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        player2 = GameObject.Find("Grayboxed Bandit(Clone)");
        target = Random.Range(1,3);
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
        offset.x = 3;
        offset.y = 3;
        offset.y = 3;
        explode = GetComponent<FlashScript>();

    }




    /// <summary>
    /// Update is called once per frame
    /// Tracks the player
    /// </summary>
    void FixedUpdate()
    {
        TrackTargetPlayer(targetObject);
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
        difference.x = Mathf.Abs(difference.x);
        difference.y = Mathf.Abs(difference.y);
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


    /// <summary>
    /// Handles enemy life loss and death due to attacks
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet")
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
            GameController gc = GameObject.Find("Game Controller").
                GetComponent<GameController>();
            if (health <= 0)
            {
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
                    healthWhileExplode--;
                    if (healthWhileExplode <= 0&&exploding!=null&&!killed)
                    {
                        StopCoroutine(exploding);
                        gc.RemoveEnemy();
                        LootTableAndDropBehavior loot = GameObject.Find("Game Controller").
                            GetComponent<LootTableAndDropBehavior>();
                        loot.DropLoot(transform.position);
                        killed = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
        if (collision.gameObject.tag == "explodey")
        {
            GameController gc = GameObject.Find("Game Controller").
                GetComponent<GameController>();
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
            if (health <= 0)
            {
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
                    healthWhileExplode--;
                    if (healthWhileExplode <= 0 && exploding != null&&!killed)
                    {
                        StopCoroutine(exploding);
                        gc.RemoveEnemy();
                        LootTableAndDropBehavior loot = GameObject.Find("Game Controller").
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
}
