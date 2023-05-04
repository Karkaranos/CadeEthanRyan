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
    public override void Shoot(GameObject attackMe)
    {
        StartCoroutine(DespawnTimer());
        StartCoroutine(CanDealDamage());
    }
}
