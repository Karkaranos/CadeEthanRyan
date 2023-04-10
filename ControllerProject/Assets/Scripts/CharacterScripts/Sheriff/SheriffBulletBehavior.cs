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
    private GameObject scope;
    private float speed = .05f;
    Vector2 moveForce = Vector2.zero;

    #endregion Variables

    #region Functions

    //Handles functions that happen upon Instantiation
    #region OnSpawn

    /// <summary>
    /// Sets the bullet's direction and adds force
    /// </summary>
    void Awake()
    {
        scope = GameObject.Find("Scope");
        Vector2 scopePos = scope.transform.position;

        //Code from robertbu on Stack Overflow- it gets the direction of the scope
        //Then converts it to an angle and sets it
        Vector3 dir = scope.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        moveForce.x = dir.x;
        moveForce.y = dir.y;
        moveForce *= speed;
        GetComponent<Rigidbody2D>().AddForce(moveForce);

    }

    #endregion OnSpawn

    //Handles collisions with other objects
    #region Collisions
    /// <summary>
    /// Handles collisions with colliders
    /// </summary>
    /// <param name="collision">The object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    #endregion Collisions

    #endregion Functions
}
