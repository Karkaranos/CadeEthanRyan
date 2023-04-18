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

    //References to players and setting targets
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;
    private int targetSwitchTimer;

    //General Variables
    [SerializeField] private float health;

    #endregion Variables

    #region Functions

    /// <summary>
    /// Called before the first frame update; sets initial target and starts attack
    /// </summary>
    void Start()
    {
        target = 1;
        player1 = GameObject.Find("Grayboxed Sheriff(Clone)");
        targetObject = player1;
        StartCoroutine(FireSpikes());
        StartCoroutine(SwitchTarget());
    }



    /// <summary>
    /// Creates a turret-like behavior and spawns a spike per a specified time
    /// </summary>
    /// <returns>How often spikes spawn</returns>
    IEnumerator FireSpikes()
    {
        GameObject objectSpawned;
        for (; ; )
        {
            attackingHead = Random.Range(1, 4);
            if (attackingHead == 1)
            {
                objectSpawned = (Instantiate(spike, atkPoint1.transform.position,
                    Quaternion.identity));
                spikesShot.Add(objectSpawned);
                objectSpawned.GetComponent<CactusSpikeBehavior>().
                    GetTarget(targetObject);
            }
            else if (attackingHead == 2)
            {
                objectSpawned = (Instantiate(spike, atkPoint2.transform.position,
                    Quaternion.identity));
                spikesShot.Add(objectSpawned);
                objectSpawned.GetComponent<CactusSpikeBehavior>().
                    GetTarget(targetObject);
            }
            else
            {
                objectSpawned = (Instantiate(spike, atkPoint3.transform.position,
                    Quaternion.identity));
                spikesShot.Add(objectSpawned);
                objectSpawned.GetComponent<CactusSpikeBehavior>().
                    GetTarget(targetObject);
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
        if (target == 1)
        {
            target = 2;
            //targetObject = player2;
        }
        else
        {
            target = 1;
            targetObject = player1;
        }
        targetSwitchTimer = Random.Range(2, 10);
        yield return new WaitForSeconds(targetSwitchTimer);
    }


    /// <summary>
    /// Checks if the Cactus has been hit by a bullet. 
    /// Upon Death, starts the Cactus's explosion and destroys all existing spikes
    /// </summary>
    /// <param name="collision">The object collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FlashScript explode = GetComponent<FlashScript>();
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            SheriffBulletBehavior sbb =
                collision.gameObject.GetComponent<SheriffBulletBehavior>();
            health -= sbb.damageDealt;
            if (health <= 0)
            {
                GameObject destroyMe;
                foreach (GameObject c in spikesShot)
                {
                    destroyMe = c;
                    Destroy(destroyMe);
                }
                StartCoroutine(explode.Kaboom(ignitionToExplode));
                GameController gc = GameObject.Find("Game Controller").
                    GetComponent<GameController>();
                gc.RemoveEnemy();
                Destroy(gameObject);
            }
        }
    }

    #endregion Functions
}
