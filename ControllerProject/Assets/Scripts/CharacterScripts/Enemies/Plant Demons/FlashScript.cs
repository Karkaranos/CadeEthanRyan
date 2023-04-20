/*****************************************************************************
// File Name :         FlashScript.cs
// Author :            Cade R. Naylor
// Creation Date :     April 4, 2023
//
// Brief Description : Makes an object flash red and white before exploding
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    Color flash1 = new Color(255, 255, 255);
    Color flash2 = new Color(255, 0, 0);
    [SerializeField] float deathTimer = 5;
    [SerializeField] float flashInterval =.67f;
    [SerializeField] GameObject explode;
    int c = 0;
    private GameObject destroyMe;
    List<GameObject> spawnedSpikes = new List<GameObject>();
    [SerializeField] private int numSpikesSpawned=12;
    [SerializeField] GameObject explodeSpike;
    Vector2 spikeTarget;
    float spawnAngle;
    Vector2 direction;
    Vector2 moveForce;


    // Start is called before the first frame update


    public virtual IEnumerator Kaboom(float explodeCountdown)
    {
        yield return new WaitForSeconds(explodeCountdown);
        destroyMe = Instantiate(explode, transform.position, transform.rotation);
        yield return new WaitForSeconds(.1f);
        for(int i = 0; i < numSpikesSpawned; i++)
        {
            spawnAngle += (360 / numSpikesSpawned);
            spawnedSpikes.Add(Instantiate(explodeSpike, transform.position,
                Quaternion.AngleAxis(spawnAngle, Vector3.forward)));

        }
        GameController gc = GameObject.Find("Game Controller").
    GetComponent<GameController>();
        gc.RemoveEnemy();
        Destroy(destroyMe);
        Destroy(gameObject);


    }
    public void Flash()
    {
        GetComponent<Renderer>().material.color = flash1;
        c = 1;
        StartCoroutine(ExplodeFlash());
    }



    public IEnumerator ExplodeFlash()
    {
        for (; ; )
        {
            if (c == 1)
            {
                GetComponent<Renderer>().material.color = flash2;
                c = 2;
            }
            else
            {
                GetComponent<Renderer>().material.color = flash1;
                c = 1;
            }

            flashInterval *= .9f;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
