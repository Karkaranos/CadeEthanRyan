using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheckBehavior : MonoBehaviour
{
    public bool isOverlapping=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOverlapping = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isOverlapping = true;
    }
}
