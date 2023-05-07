/*****************************************************************************
// File Name :         FireBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Handles the range and destruction of the cockail's fire
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    Vector3 scale;
    float secondsToWait = .02f;
    public float damageDealt;
    public bool shotByPlayer;

    /// <summary>
    /// Start is called before the first frame. Starts the despawn timer.
    /// </summary>
    void Start()
    {
        StartCoroutine(FireGone());
    }

    /// <summary>
    /// Shrinks the range of fire and deletes it after a set time
    /// </summary>
    /// <returns>How long in between shrinks</returns>
    IEnumerator FireGone()
    {
        for(; ; )
        {
            scale = transform.localScale;
            scale *= 1.1f;
            transform.localScale = scale;
            yield return new WaitForSeconds(secondsToWait);
            if (scale.magnitude >= 4)
            {
                yield return new WaitForSeconds(2.7f);
                Destroy(gameObject);
            }
        }
    }

}
