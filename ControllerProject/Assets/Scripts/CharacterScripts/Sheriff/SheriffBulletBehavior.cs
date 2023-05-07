/*****************************************************************************
// File Name :         SheriffBulletBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 1, 2023
//
// Brief Description : Sets a direction for Sheriff's bullets and adds force
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffBulletBehavior : MonoBehaviour
{
    #region Variables

    //General variables for setting speed and direction
    GameObject target;
    private float speed = .1f;
    Vector2 moveForce = Vector2.zero;
    public bool shotByPlayer=true;

    //Storing the damage this deals
    public float damageDealt;

    #endregion Variables

    #region Functions

    /// <summary>
    /// Sets the bullet's direction and adds force
    /// </summary>
    public void Shoot(GameObject attackMe)
    {
        target = attackMe;

        //Code from robertbu on Stack Overflow- it gets the direction of the scope
        //Then converts it to an angle and sets it
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Sets the move force and speed
        moveForce.x = dir.x;
        moveForce.y = dir.y;
        moveForce *= speed;
        GetComponent<Rigidbody2D>().AddForce(moveForce);
        StartCoroutine(DespawnTimer());
    }



    /// <summary>
    /// Destrous the bullet after a set time to reduce lag
    /// </summary>
    /// <returns>How long it waits before destroying</returns>
    public IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles collisions with colliders
    /// </summary>
    /// <param name="collision">The object collided with</param>
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// Handles collisions with triggers
    /// </summary>
    /// <param name="collision">The object collided with</param>
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //If colliding with enemy or world object (or player if not shot by player)
        //destroy
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag ==
            "World Objects" || (collision.gameObject.tag == "player" &&
            !shotByPlayer && damageDealt > 0))
        {
            Destroy(gameObject);
        }
    }
    #endregion Functions
}
