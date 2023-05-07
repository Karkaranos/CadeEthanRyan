/*****************************************************************************
// File Name :         DamageStoreExplodeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Stores explosion damage
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStoreExplodeBehavior : MonoBehaviour
{
    #region Variables
    public float damageDealt;
    public bool shotByPlayer = true;
    #endregion

    //Handles explosion destruction
    #region Functions

    /// <summary>
    /// Happens when object is instantiated; starts death timer
    /// </summary>
    private void Awake()
    {
        StartCoroutine(DeathTimer());
    }

    /// <summary>
    /// Destroys the explosion after a set amount of time
    /// </summary>
    /// <returns>Time waited before destruction</returns>
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }

    #endregion
}


