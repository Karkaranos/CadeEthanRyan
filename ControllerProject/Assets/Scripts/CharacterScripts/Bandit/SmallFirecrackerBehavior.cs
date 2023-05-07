/*****************************************************************************
// File Name :         SmallFirecrackerBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Handles explosions for the Small Firecracker
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFirecrackerBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject kaboom;
    public float damageDealt;
    GameObject destroyThisObject;
    public bool shotByPlayer = true;

    Color flash1 = new Color(255, 255, 255);
    Color flash2 = new Color(255, 0, 0);
    [SerializeField] float deathTimer = 5;
    [SerializeField] float flashInterval = .67f;
    [SerializeField] GameObject explode;
    int c = 0;
    [SerializeField] AudioClip firecrackerBoom;
    #endregion

    #region Functions

    /// <summary>
    /// Handles explosions for the small Firecracker
    /// </summary>
    /// <param name="explodeCountdown">How long until it explodes</param>
    /// <returns>Time waited</returns>
    public IEnumerator Kaboom(float explodeCountdown)
    {
        //Wait, then play an explosion sound
        yield return new WaitForSeconds(explodeCountdown);
        AudioSource.PlayClipAtPoint(firecrackerBoom, transform.position, .4f);

        //Create an explosion
        destroyThisObject =Instantiate(kaboom, transform.position, Quaternion.
            identity);
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().shotByPlayer
            = shotByPlayer;
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().damageDealt 
            = damageDealt;

        //Wait, then destroy this object
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    /// <summary>
    /// Starts the flash coroutine and sets initial color
    /// </summary>
    /// <param name="countdown"></param>
    public void Flash(float countdown)
    {
        GetComponent<Renderer>().material.color = flash1;
        c = 1;
        StartCoroutine(ExplodeFlash());
        StartCoroutine(Kaboom(countdown));
    }

    /// <summary>
    /// Flashes the weapon between two colors, speeding up each time
    /// </summary>
    /// <returns></returns>
    public IEnumerator ExplodeFlash()
    {
        for (; ; )
        {
            if (c == 1)
            {
                //Sets the color to red
                GetComponent<Renderer>().material.color = flash2;
                c = 2;
            }
            else
            {
                //sets the color to white
                GetComponent<Renderer>().material.color = flash1;
                c = 1;
            }

            //Speeds up slightly each time, then waits
            flashInterval *= .9f;
            yield return new WaitForSeconds(flashInterval);
        }
    }
    #endregion
}
