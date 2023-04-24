/*****************************************************************************
// File Name :         ExplodeSpikeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 8, 2023
//
// Brief Description : Adds force to spikes spawned from explosions
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSpikeBehavior : MonoBehaviour
{
    #region Variables

    Vector2 spikeTarget;
    [SerializeField] private float despawnTime = 2;
    #endregion

    #region Functions


    /// <summary>
    /// Starts the despawn timer and calls target setting
    /// </summary>
    void Start()
    {
        StartCoroutine(DespawnTimer());
        SetTarget();
    }

    /// <summary>
    /// Picks a random location within 10 units of the player and sets it as the 
    /// target. Adds force in that direction.
    /// </summary>
    public void SetTarget()
    {
        int dir = Random.Range(1, 3);
        spikeTarget.x = Random.Range((transform.position.x + 3),
    (transform.position.x + 10));
        if (dir == 1)
        {
            spikeTarget.x *= -1;
        }

        dir = Random.Range(1, 3);
        spikeTarget.y = Random.Range((transform.position.y + 3),
    (transform.position.y + 10));
        if (dir == 1)
        {
            spikeTarget.y *= -1;
        }
        spikeTarget *= .01f;
        GetComponent<Rigidbody2D>().AddForce(spikeTarget);
    }

    /// <summary>
    /// Controls how long before the object is destroyed
    /// </summary>
    /// <returns>Seconds before destruction</returns>
    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    /// <summary>
    /// Checks for collisions
    /// </summary>
    /// <param name="collision"></param>
    /// 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" || collision.gameObject.tag == 
            "World Objects")
        {
            Destroy(gameObject);
        }
    }

    #endregion Functions
}
