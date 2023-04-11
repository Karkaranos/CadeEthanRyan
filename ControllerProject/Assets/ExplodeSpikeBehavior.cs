/*****************************************************************************
// File Name :         ExplodeSpikeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 8, 2023
//
// Brief Description : Adds force to spikes spawned from explosions
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSpikeBehavior : MonoBehaviour
{
    #region Variables

    Rigidbody2D rb2D;
    Vector2 velocity;
    [SerializeField] private float despawnTime = 2;
    #endregion

    #region Functions


    // Start is called before the first frame update
    void Start()
    {
        //Add forward force
        StartCoroutine(DespawnTimer());
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }
    }

    #endregion Functions
}
