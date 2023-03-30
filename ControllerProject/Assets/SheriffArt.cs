using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffArt : MonoBehaviour
{
    public void SetDirection(Vector2 pos, Quaternion rot)
    {
        if (pos.x > 0.05 && pos.x < 1)
        {
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
        else if (pos.x < -0.05 && pos.x > -1)
        {
            GetComponent<Renderer>().material.color = new Color(255, 255, 0);
        }
        else if (pos.y > 0 && pos.y < 1)
        {
            GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        }
        else if (pos.y < 0 && pos.y > -1)
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 255);
        }

    }
}
