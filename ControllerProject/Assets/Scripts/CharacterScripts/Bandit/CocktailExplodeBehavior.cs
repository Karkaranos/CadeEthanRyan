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


    public virtual IEnumerator Kaboom(float explodeCountdown)
    {
        yield return new WaitForSeconds(explodeCountdown);
        AudioSource.PlayClipAtPoint(molotovThrow, transform.position,
            .9f);
        temp =Instantiate(fire, transform.position, transform.rotation);
        temp.GetComponent<FireBehavior>().shotByPlayer = shotByPlayer;
        temp.GetComponent<FireBehavior>().damageDealt = cDamageDealt / 2;
        Destroy(gameObject);
    }
    public void Flash()
    {
        GetComponent<Renderer>().material.color = flash1;
        c = 1;
        StartCoroutine(ExplodeFlash());
    }



    public IEnumerator ExplodeFlash()
    {
        for (; ; )
        {
            if (c == 1)
            {
                GetComponent<Renderer>().material.color = flash2;
                c = 2;
            }
            else
            {
                GetComponent<Renderer>().material.color = flash1;
                c = 1;
            }

            flashInterval *= .9f;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
