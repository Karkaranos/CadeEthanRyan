/*****************************************************************************
// File Name :         CocktailExplodeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Moves the cocktail, starts its timer, and explodes it
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailExplodeBehavior : MonoBehaviour
{
    #region Variables
    Color flash1 = new Color(255, 255, 255);
    Color flash2 = new Color(255, 0, 0);
    [SerializeField] float deathTimer = 5;
    [SerializeField] float flashInterval = .67f;
    [SerializeField] GameObject fire;
    int c = 0;
    public float cDamageDealt;
    GameObject temp;
    public bool shotByPlayer = true;
    [SerializeField] AudioClip molotovThrow;
    #endregion

    #region Functions

    /// <summary>
    /// Handles exploding for Molotovs
    /// </summary>
    /// <param name="explodeCountdown">How long before explosion</param>
    /// <returns>Time waited</returns>
    public virtual IEnumerator Kaboom(float explodeCountdown)
    {
        //Wait to explode, then spawn explosion and play audio
        yield return new WaitForSeconds(explodeCountdown);
        AudioSource.PlayClipAtPoint(molotovThrow, transform.position,
            .9f);

        //Create and initialize a fire explosion
        temp =Instantiate(fire, transform.position, transform.rotation);
        temp.GetComponent<FireBehavior>().shotByPlayer = shotByPlayer;
        temp.GetComponent<FireBehavior>().damageDealt = cDamageDealt / 2;
        Destroy(gameObject);
    }

    /// <summary>
    /// Sets the initial color and starts weapon flash
    /// </summary>
    public void Flash()
    {
        GetComponent<Renderer>().material.color = flash1;
        c = 1;
        StartCoroutine(ExplodeFlash());
    }


    /// <summary>
    /// Alters the sprite's color between red and white while speeding up
    /// </summary>
    /// <returns></returns>
    public IEnumerator ExplodeFlash()
    {
        for (; ; )
        {
            if (c == 1)
            {
                //Set the color to red
                GetComponent<Renderer>().material.color = flash2;
                c = 2;
            }
            else
            {
                //set the color to white
                GetComponent<Renderer>().material.color = flash1;
                c = 1;
            }

            //speed up how fast it flashes
            flashInterval *= .9f;
            yield return new WaitForSeconds(flashInterval);
        }
    }

    #endregion
}
