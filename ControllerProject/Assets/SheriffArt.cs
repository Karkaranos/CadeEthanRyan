using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffArt : MonoBehaviour
{
    void SetDirection(Vector2 pos)
    {
        if (pos.x > 0.05 && pos.x < 1)
        {
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            playerRot.z = 0f;

        }
        else if (pos.x < -0.05 && pos.x > -1)
        {
            GetComponent<Renderer>().material.color = new Color(255, 255, 0);
            playerRot.z = 180f;
        }
        else if (pos.y > 0 && pos.y < 1)
        {
            GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            playerRot.z = 90f;
        }
        else if (pos.y < 0 && pos.y > -1)
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 255);
            playerRot.z = 270f;
        }
    }
}
