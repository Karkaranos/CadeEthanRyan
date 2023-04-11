/*****************************************************************************
// File Name :         SmallTumbleFiendBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 3, 2023
//
// Brief Description : Has the override for small Tumblefiends on death
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits from Large TumbleFiend Behavior
public class SmallTumbleFiendBehavior : LargeTumbleFiendBehavior
{
    //Contains Overrides for Large TumbleFiends
    #region Overrides
    /// <summary>
    /// On death, destroys the TumbleFiend
    /// </summary>
    public override void OnDeath()
    {
        Destroy(gameObject);
    }
    #endregion
}
