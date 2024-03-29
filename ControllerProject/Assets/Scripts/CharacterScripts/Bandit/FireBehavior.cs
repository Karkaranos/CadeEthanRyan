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
    #region Variables
    Vector3 scale;
    float secondsToWait = .02f;
    public float damageDealt;
    public bool shotByPlayer;
    #endregion

    #region Functions
    /// <summary>
    /// Start is called before the first frame. Starts the despawn timer.
    /// </summary>
    void Start()
    {
        StartCoroutine(FireGone());
    }

    /// <summary>
    /// Increases the range of fire and deletes it after a set time
    /// </summary>
    /// <returns>How long in between growth</returns>
    IEnumerator FireGone()
    {
        for(; ; )
        {
            //Get the current scale and increase it
            scale = transform.localScale;
            scale *= 1.1f;
            transform.localScale = scale;
            yield return new WaitForSeconds(secondsToWait);

            //If the current fire is larger than 4, wait and destroy it
            if (scale.magnitude >= 4)
            {
                yield return new WaitForSeconds(2.7f);
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
