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
    public float damageDealt;

    public override IEnumerator Kaboom(float explodeCountdown)
    {
        yield return new WaitForSeconds(explodeCountdown);
        destroyThisObject = Instantiate(kaboom, transform.position, 
            transform.rotation);
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().damageDealt =
            damageDealt;
        yield return new WaitForSeconds(.1f);
        Destroy(destroyThisObject);
        Destroy(gameObject);
    }
}
