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
    [SerializeField] private GameObject scope;
    private Quaternion targetAngle;
    private float targetAng;
    Vector2 pos;

    private float speed = .1f;
    void Awake()
    {
        scope = GameObject.Find("Scope");
        Vector2 scopePos = scope.transform.position;
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;
        Vector2 direction;
        //float hyp;

        pos = (scope.transform.position - transform.position);
        //hyp = Mathf.Sqrt((Mathf.Pow(pos.x, 2)) +(Mathf.Pow(pos.y, 2)));

        targetAng = Mathf.Rad2Deg;
        direction.y = (Mathf.Cos(targetAng));
        direction.x = (Mathf.Sin(targetAng));

        targetAng = Mathf.Atan(pos.x / pos.y);
        print(targetAng);


        direction.y = (Mathf.Cos(targetAng));
        direction.x = (Mathf.Sin(targetAng));
        difference.x = scopePos.x - transform.position.x;
        difference.y = scopePos.y - transform.position.y;
        moveForce.x = 1;
        if (difference.y < -1)
        {
            direction.y *= -1;
        }

        moveForce.x = direction.x;
        moveForce.y = direction.y;

        direction.x = Mathf.Rad2Deg;
        direction.y = Mathf.Rad2Deg;
        if (difference.y < -1)
        {
            direction.y *= -1;
        }
        if (difference.x < 0)
        {
            direction.x *= -1;
        }
        targetAngle.x = direction.x;
        targetAngle.y = direction.y;


        Debug.Log(targetAng);
        Debug.Log(direction.x + " " + direction.y);
        moveForce *= speed;
        transform.rotation = targetAngle;

        GetComponent<Rigidbody2D>().AddForce(moveForce);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
