/*****************************************************************************
// File Name :         BanditExplodeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 1, 2023
//
// Brief Description : Moves the explosive, starts its timer, and explodes it
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditExplodeBehavior : FlashScript
{
    #region Variables
    [SerializeField] GameObject kaboom;
    GameObject destroyThisObject;
    public float bDamageDealt;
    public bool bShotByPlayer = true;
    [SerializeField] AudioClip basicBoom;
    #endregion

    //Handles variable set up and Overrides for FlashScript
    #region Set up and Function Overrides

    /// <summary>
    /// Called before the first frame; initializes variables
    /// </summary>
    private void Start()
    {
        damageDealt = bDamageDealt;
        shotByPlayer = bShotByPlayer;
    }

    /// <summary>
    /// A version of 'Kaboom' with information specifically for Dynamite Explosions
    /// </summary>
    /// <param name="explodeCountdown">Time before explosion</param>
    /// <returns>Time before explosion</returns>
    public override IEnumerator Kaboom(float explodeCountdown)
    {
        //Wait for explosion countdown, then play audio
        yield return new WaitForSeconds(explodeCountdown);
        AudioSource.PlayClipAtPoint(basicBoom, transform.position, 3f);

        //Instantiate an explosion and initialize it
        destroyThisObject = Instantiate(kaboom, transform.position, 
            transform.rotation);
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().shotByPlayer =
            shotByPlayer;
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().damageDealt =
            damageDealt;

        //Destroy the weapon
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    #endregion
}
