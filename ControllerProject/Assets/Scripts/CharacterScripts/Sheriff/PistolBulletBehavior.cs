/*****************************************************************************
// File Name :         PistolBulletBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 25, 2023
//
// Brief Description : Derived from SheriffBulletBehavior; contains overrides for
                        collisions
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletBehavior : SheriffBulletBehavior
{
    #region Overrides
    /// <summary>
    /// Overrides all collisions for Pistol
    /// </summary>
    /// <param name="collision">object collided with</param>
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //Do Nothing
    }

    /// <summary>
    /// Overrides all collisions for Pistol
    /// </summary>
    /// <param name="collision">object collided with</param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //Do Nothing
    }
    #endregion
}
