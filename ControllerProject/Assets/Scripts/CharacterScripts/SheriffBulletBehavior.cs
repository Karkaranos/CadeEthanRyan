using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffBulletBehavior : MonoBehaviour
{
    [SerializeField] private GameObject scope;
    private int speed = 10;
    void Start()
    {
        scope = GameObject.Find("Scope");
        Vector2 scopePos = scope.transform.position;
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;

        difference.x = scopePos.x - transform.position.x;
        difference.y = scopePos.y - transform.position.y;
        if (difference.x < 0)
        {
            moveForce.x += -1 * speed * Time.deltaTime;
        }
        else
        {
            moveForce.x += 1 * speed * Time.deltaTime;
        }
        if (difference.y < 0)
        {
            moveForce.y += -1 * speed * Time.deltaTime;
        }
        else
        {
            moveForce.y += 1 * speed * Time.deltaTime;
        }
        GetComponent<Rigidbody2D>().AddForce(moveForce);
    }
}
