using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFirecrackerBehavior : MonoBehaviour
{
    [SerializeField] GameObject kaboom;
    public float damageDealt;
    GameObject destroyThisObject;

    Color flash1 = new Color(255, 255, 255);
    Color flash2 = new Color(255, 0, 0);
    [SerializeField] float deathTimer = 5;
    [SerializeField] float flashInterval = .67f;
    [SerializeField] GameObject explode;
    int c = 0;



    // Start is called before the first frame update


    public IEnumerator Kaboom(float explodeCountdown)
    {
        yield return new WaitForSeconds(explodeCountdown);
        destroyThisObject=Instantiate(kaboom, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.1f);
        Destroy(destroyThisObject);
        Destroy(gameObject);
    }
    public void Flash(float countdown)
    {
        GetComponent<Renderer>().material.color = flash1;
        c = 1;
        StartCoroutine(ExplodeFlash());
        StartCoroutine(Kaboom(countdown));
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
