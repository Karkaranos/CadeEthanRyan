using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTumbleFiendBehavior : LargeTumbleFiendBehavior
{
    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
