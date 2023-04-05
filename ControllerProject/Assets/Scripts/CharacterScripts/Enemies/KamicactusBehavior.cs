/*****************************************************************************
// File Name :         KamicactusBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 3, 2023
//
// Brief Description : Kamicactus Enemy Type
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamicactusBehavior : MonoBehaviour
{
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    Vector3 offset;
    [SerializeField] private float speed = 6f;
    FlashScript explode;
    private float ignitionToExplode = 5;
    private bool explodeStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        target = 1;
        offset.x = 3;
        offset.y = 3;
        offset.y = 3;
        explode = GetComponent<FlashScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target == 1)
        {
            targetObject = player1;
        }
        TrackTargetPlayer(targetObject);
    }

    void TrackTargetPlayer(GameObject target)
    {
        Vector2 targetPos=target.transform.position;
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;

        difference.x = targetPos.x - transform.position.x;
        difference.y = targetPos.y - transform.position.y;
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
        transform.Translate(moveForce, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (!explodeStarted)
            {
                explode.Flash();
                StartCoroutine(explode.Kaboom(ignitionToExplode));
                explodeStarted = true;
            }
        }
    }
}
