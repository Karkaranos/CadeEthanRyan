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

    #endregion Variables

    #region Functions

    //Initializes basic variables and starts Coroutines
    #region SetUp
    /// <summary>
    /// Called before the first frame update; sets initial target and starts attack
    /// </summary>
    void Start()
    {
        target = 1;
        targetObject = player1;
        StartCoroutine(FireSpikes());
        StartCoroutine(SwitchTarget());
    }
    #endregion SetUp

    //Handles the turret behavior and enemy death
    #region Attacks and Death
    /// <summary>
    /// Creates a turret-like behavior and spawns a spike per a specified time
    /// </summary>
    /// <returns>How often spikes spawn</returns>
    IEnumerator FireSpikes()
    {
        for(; ; )
        {
            attackingHead = Random.Range(1, 4);
            if (attackingHead == 1)
            {
                spikesShot.Add(Instantiate(spike, atkPoint1.transform.position,
                    Quaternion.identity));
            }
            else if (attackingHead == 2)
            {
                spikesShot.Add(Instantiate(spike, atkPoint2.transform.position,
                    Quaternion.identity));
            }
            else
            {
                spikesShot.Add(Instantiate(spike, atkPoint3.transform.position,
                    Quaternion.identity));
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
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            GameObject destroyMe;
            foreach(GameObject c in spikesShot)
            {
                destroyMe = c;
                Destroy(destroyMe);
            }
        }
        explode.Flash();
        StartCoroutine(explode.Kaboom(ignitionToExplode));
        Destroy(gameObject);
    }
    #endregion Attacks and Death

    #endregion Functions
}
