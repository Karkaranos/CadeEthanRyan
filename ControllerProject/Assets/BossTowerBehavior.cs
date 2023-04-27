using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTowerBehavior : MonoBehaviour
{
    private float health=20;
    BossBehavior bb;
    private bool destroyed;
    // Start is called before the first frame update
    void Start()
    {
        bb = GameObject.Find("Grayboxed Boss").GetComponent<BossBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            bb.RemoveTower(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (collision.name.Contains("Pistol"))
            {
                PistolBulletBehavior pbb =
                    collision.gameObject.GetComponent<PistolBulletBehavior>();
                health -= pbb.damageDealt;

            }
            if (collision.name.Contains("Revolver"))
            {
                SheriffBulletBehavior sbb =
                    collision.gameObject.GetComponent<SheriffBulletBehavior>();
                health -= sbb.damageDealt;
            }
            if (collision.name.Contains("Spray"))
            {
                SprayShotgunBulletBehavior ssbb =
                    collision.GetComponent<SprayShotgunBulletBehavior>();
                health -= ssbb.damageDealt;
            }
            if (collision.name.Contains("Shotgun"))
            {
                ShotgunBulletBehavior shotbb =
                    collision.gameObject.GetComponent<ShotgunBulletBehavior>();
                health -= shotbb.damageDealt;
            }
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
            }
        }
        if (collision.gameObject.tag == "explodey")
        {
            if (collision.name.Contains("Fire"))
            {
                FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();
                health -= fb.damageDealt;
            }
            else if (collision.name.Contains("Kaboom"))
            {
                DamageStoreExplodeBehavior dseb = collision.gameObject.
                    GetComponent<DamageStoreExplodeBehavior>();
                health -= dseb.damageDealt;
            }
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
            }
        }

    }
}
