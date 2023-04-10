using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSpikeBehavior : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
        velocity = rb2D.velocity;
        velocity.x += 1;
        velocity.y += 1;
        velocity *= Time.deltaTime;
        rb2D.AddForce(velocity);
    }
}
