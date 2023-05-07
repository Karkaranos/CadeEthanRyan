/*****************************************************************************
// File Name :         SprayShotgunBulletBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 25, 2023
//
// Brief Description : Derived from ShotgunBulletBehavior; contains overrides for
                        on awake
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayShotgunBulletBehavior : ShotgunBulletBehavior
{
    /// <summary>
    /// Handles overrides for Shotgun Bullet Behavior
    /// </summary>
    /// <param name="attackMe">Target object-not used</param>
    public override void Shoot(GameObject attackMe)
    {
        StartCoroutine(DespawnTimer());
    }
}
