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
    [SerializeField] GameObject kaboom;
    GameObject destroyThisObject;
    public float bDamageDealt;
    public bool bShotByPlayer = true;
    [SerializeField] AudioClip basicBoom;

    public override IEnumerator Kaboom(float explodeCountdown)
    {
        yield return new WaitForSeconds(explodeCountdown);
        AudioSource.PlayClipAtPoint(basicBoom, transform.position, 2f);
        destroyThisObject = Instantiate(kaboom, transform.position, 
            transform.rotation);
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().shotByPlayer =
            shotByPlayer;
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().damageDealt =
            damageDealt;
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
