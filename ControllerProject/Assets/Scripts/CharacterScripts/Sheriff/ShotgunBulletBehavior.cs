/*****************************************************************************
// File Name :         ShotgunBulletBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 25, 2023
//
// Brief Description : Sets a direction for Shotgun bullets and adds force, as well
                        as spawn two additional bullets
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletBehavior : MonoBehaviour
{
    #region Variables

    //General variables for setting speed and direction
    GameObject target;
    private float speed = .1f;
    Vector2 moveForce = Vector2.zero;
    [SerializeField] GameObject bulletSpray;
    GameObject temp;

    //Storing the damage this deals
    public float damageDealt;

    #endregion Variables

    #region Functions


    /// <summary>
    /// Sets the bullet's direction and adds force
    /// </summary>
    public virtual void Shoot(GameObject attackMe)
    {
        target = attackMe;

        //Code from robertbu on Stack Overflow- it gets the direction of the scope
        //Then converts it to an angle and sets it
        Vector3 dir = attackMe.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Spawns one offshoot bullet above/to the right of the main bullet and adds
        //force
        moveForce.x = dir.x + .25f;
        moveForce.y = dir.y + .25f;
        moveForce *= speed;
        angle += 30;
        temp = Instantiate(bulletSpray, transform.position,
            Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<Rigidbody2D>().AddForce(moveForce);


        //Spawns one offshoot bullet below/to the left of the main bullet and adds
        //force
        moveForce.x = dir.x - .25f;
        moveForce.y = dir.y - .25f;
        moveForce *= speed;
        angle -= 60;
        temp = Instantiate(bulletSpray, transform.position,
            Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<Rigidbody2D>().AddForce(moveForce);


        //Adds force to the main bullet
        moveForce.x = dir.x;
        moveForce.y = dir.y;
        moveForce *= speed;
        GetComponent<Rigidbody2D>().AddForce(moveForce);
        StartCoroutine(DespawnTimer());
    }
    /*public virtual void Awake()
    {
        scope = GameObject.Find("Scope");
        Vector2 scopePos = scope.transform.position;

        //Code from robertbu on Stack Overflow- it gets the direction of the scope
        //Then converts it to an angle and sets it
        Vector3 dir = scope.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Spawns one offshoot bullet above/to the right of the main bullet and adds
        //force
        moveForce.x = dir.x + .25f;
        moveForce.y = dir.y + .25f;
        moveForce *= speed;
        angle += 30;
        temp=Instantiate(bulletSpray, transform.position, 
            Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<Rigidbody2D>().AddForce(moveForce);


        //Spawns one offshoot bullet below/to the left of the main bullet and adds
        //force
        moveForce.x = dir.x-.25f;
        moveForce.y = dir.y-.25f;
        moveForce *= speed;
        angle -= 60;
        temp=Instantiate(bulletSpray, transform.position,
            Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<Rigidbody2D>().AddForce(moveForce);


        //Adds force to the main bullet
        moveForce.x = dir.x;
        moveForce.y = dir.y;
        moveForce *= speed;
        GetComponent<Rigidbody2D>().AddForce(moveForce);
        StartCoroutine(DespawnTimer());
    }*/

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
    public void OnCollisionEnter2D(Collision2D collision)
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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "World Objects")
        {
            Destroy(gameObject);
        }
    }

    #endregion Functions
}

