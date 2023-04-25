using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayShotgunBulletBehavior : ShotgunBulletBehavior
{
    public override void Awake()
    {
        StartCoroutine(DespawnTimer());
    }
}
