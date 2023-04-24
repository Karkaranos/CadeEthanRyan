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
    //[SerializeField] GameObject player2;
    Vector3 offset;



    //Variables for exploding
    FlashScript explode;
    private float ignitionToExplode = 5;
    private bool explodeStarted = false;
    [SerializeField] private int explosionSize = 3;
    [SerializeField] GameObject explodeRange;
    public bool killed=false;

    #endregion

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
        explode = GetComponent<FlashScript>();

    }




    /// <summary>
    /// Update is called once per frame
    /// Tracks the player
    /// </summary>
    void FixedUpdate()
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
    /// Handles enemy life loss and death due to attacks
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            GameController gc = GameObject.Find("Game Controller").
                GetComponent<GameController>();
            SheriffBulletBehavior sbb =
                collision.gameObject.GetComponent<SheriffBulletBehavior>();
            health -= sbb.damageDealt;
            if (health <= 0)
            {
                if (!explodeStarted)
                {
                    explode.Flash();
                    exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
                    explodeStarted = true;
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
        if (collision.gameObject.name == "Kaboom(Clone)")
        {
            GameController gc = GameObject.Find("Game Controller").
                GetComponent<GameController>();
            BanditExplodeBehavior beb=
                collision.gameObject.GetComponent<BanditExplodeBehavior>();
            health -= beb.damageDealt;
            if (health <= 0)
            {
                if (!explodeStarted)
                {
                    explode.Flash();
                    exploding = StartCoroutine(explode.Kaboom(ignitionToExplode));
                    explodeStarted = true;
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
