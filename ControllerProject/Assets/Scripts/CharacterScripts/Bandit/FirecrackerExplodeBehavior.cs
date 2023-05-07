/*****************************************************************************
// File Name :         FirecrackerExplodeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Moves the explosive, starts its timer, and explodes it, 
                        spawning some smaller explosions at random
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrackerExplodeBehavior : FlashScript
{
    #region Variables
    [SerializeField] GameObject kaboom;
    [SerializeField] GameObject smallerKabooms;
    private int smallerExplosionsSpawned=5;
    public float fDamageDealt;
    GameObject destroyThisObject;
    Vector3 scale;
    Vector2 smallExplodePos;
    List<GameObject> smallExplosions = new List<GameObject>();
    public bool fShotByPlayer=true;

    #endregion Variables

    //Handles overrides for class FlashScript
    #region Function Overrides


    public override IEnumerator Kaboom(float explodeCountdown)
    {
        yield return new WaitForSeconds(explodeCountdown);
        scale = transform.localScale;
        destroyThisObject = Instantiate(kaboom, transform.position, transform.
            rotation);
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().shotByPlayer =
            shotByPlayer;
        destroyThisObject.GetComponent<DamageStoreExplodeBehavior>().damageDealt = 
            fDamageDealt;
        yield return new WaitForSeconds(.2f);
        scale = Vector3.zero;
        transform.localScale = scale;
        for (int i=0; i<smallerExplosionsSpawned; i++)
        {
            smallExplodePos.x = transform.position.x + Random.Range(-1f, 1f);
            smallExplodePos.y = transform.position.y + Random.Range(-1f, 1f);
            smallExplosions.Add(Instantiate(smallerKabooms, smallExplodePos, 
                Quaternion.identity));
        }
        foreach(GameObject i in smallExplosions)
        {
            i.GetComponent<SmallFirecrackerBehavior>().damageDealt = fDamageDealt / 
                5;
            i.GetComponent<SmallFirecrackerBehavior>().shotByPlayer = shotByPlayer;
            i.GetComponent<SmallFirecrackerBehavior>().Flash(2f);
        }
        Destroy(gameObject);
    }
    #endregion
}
