using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStoreExplodeBehavior : MonoBehaviour
{
    public float damageDealt;
    public bool shotByPlayer = true;

    private void Awake()
    {
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}


