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

    Rigidbody2D rb2D;
    float angle;
    Vector2 newPos;
    Vector2 spikeTarget;
    [SerializeField] private float despawnTime = 2;
    #endregion

    #region Functions


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnTimer());
        SetTarget();
    }

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
        spikeTarget *= .015f;
        GetComponent<Rigidbody2D>().AddForce(spikeTarget);
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }

    #endregion Functions
}
