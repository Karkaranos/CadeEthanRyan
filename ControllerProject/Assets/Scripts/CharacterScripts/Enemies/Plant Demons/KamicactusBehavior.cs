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

    #endregion

    #region Functions

    //Sets up basic function for the enemy
    #region SetUp
    /// <summary>
    /// Start is called before the first frame
    /// Sets enemy target and initial values for a couple variables
    /// </summary>
    void Start()
    {
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
    #endregion SetUp

    //Selects a target, moves towards it, and triggers its attack
    #region Movement and Attack
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
        Vector2 targetPos=target.transform.position;
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
    /// Starts the exploding process when it collides with a player
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (!explodeStarted)
            {
                explode.Flash();
                StartCoroutine(explode.Kaboom(ignitionToExplode));
                explodeStarted = true;
            }
        }
    }

    /// <summary>
    /// Handles enemy life loss and death due to attacks
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            SheriffBulletBehavior sbb =
                collision.gameObject.GetComponent<SheriffBulletBehavior>();
            health -= sbb.damageDealt;
            if (health <= 0 && !explodeStarted)
            {
                explode.Flash();
                StartCoroutine(explode.Kaboom(ignitionToExplode));
                explodeStarted = true;
            }
        }
    }
    #endregion Movement and Attack

    #endregion
}
