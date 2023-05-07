/*****************************************************************************
// File Name :         FlashScript.cs
// Author :            Cade R. Naylor
// Creation Date :     April 4, 2023
//
// Brief Description : Makes an object flash red and white before exploding
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    #region Variables

    //Variables for color and flash timers
    Color flash1 = new Color(255, 255, 255);
    Color flash2 = new Color(255, 0, 0);
    [SerializeField] float deathTimer = 5;
    [SerializeField] float flashInterval =.67f;
    [SerializeField] GameObject explode;

    //Variables for spikes and damage
    int c = 0;
    [SerializeField] GameObject destroyMe;
    List<GameObject> spawnedSpikes = new List<GameObject>();
    [SerializeField] private int numSpikesSpawned=12;
    [SerializeField] GameObject explodeSpike;
    float spawnAngle;
    public float damageDealt;
    public bool shotByPlayer=false;
    public GameObject spawnedBy;
    Quaternion angle;
    [SerializeField] AudioClip explosion;

    #endregion

    //Handles flashing and explosion
    #region Functions


    /// <summary>
    /// Starts a countdown until the enemy explodes. Spawns spikes upon explosion.
    /// </summary>
    /// <param name="explodeCountdown">How long until it explodes</param>
    /// <returns>The total time waited for</returns>
    public virtual IEnumerator Kaboom(float explodeCountdown)
    {
        //Initial explosion wait
        yield return new WaitForSeconds(explodeCountdown);

        //Spawns an explosion and sets its damage amount
        destroyMe = Instantiate(explode, transform.position, transform.rotation);
        destroyMe.GetComponent<DamageStoreExplodeBehavior>().shotByPlayer = 
            shotByPlayer;
        destroyMe.GetComponent<DamageStoreExplodeBehavior>().damageDealt = 
            damageDealt;
        AudioSource.PlayClipAtPoint(explosion, transform.position, 3f);
        //StartCoroutine(CheckForNull());
        yield return new WaitForSeconds(.2f);
        transform.localScale = Vector3.zero;
        //Spawns spikes that explode outward and sets their damage amount
        for(int i = 0; i < numSpikesSpawned; i++)
        {
            spawnAngle += (360 / numSpikesSpawned);
            spawnedSpikes.Add(Instantiate(explodeSpike, transform.position,
                Quaternion.AngleAxis(spawnAngle, Vector3.forward)));
            spawnedSpikes[i].GetComponent<ExplodeSpikeBehavior>().damageDealt = 
                damageDealt;
            spawnedSpikes[i].GetComponent<ExplodeSpikeBehavior>().angle = 
                spawnAngle;
        }

        //Destroy the enemy and explosion
        GameController gc = GameObject.Find("Game Controller").
            GetComponent<GameController>();
        gc.RemoveEnemy();
        Destroy(gameObject);
    }

    /// <summary>
    /// Starts the enemy's flash
    /// </summary>
    public void Flash()
    {
        GetComponent<Renderer>().material.color = flash1;
        c = 1;
        StartCoroutine(ExplodeFlash());
    }


    IEnumerator CheckForNull()
    {
        for(; ; )
        {
            if (spawnedBy == null)
            {
                //Destroy(destroyMe);
            }
            yield return new WaitForSeconds(.01f);
        }

    }

    /// <summary>
    /// Flashes the enemy's material between red and white while speeding up how
    /// fast it flashes each time
    /// </summary>
    /// <returns>the time between flashes</returns>
    public IEnumerator ExplodeFlash()
    {
        for (; ; )
        {
            //if its current color is white, make it red
            if (c == 1)
            {
                GetComponent<Renderer>().material.color = flash2;
                c = 2;
            }
            //if its current color is red, make it white
            else
            {
                GetComponent<Renderer>().material.color = flash1;
                c = 1;
            }

            //reduce the amount of time between flashes
            flashInterval *= .9f;
            yield return new WaitForSeconds(flashInterval);
        }
    }
    #endregion
}
