/*****************************************************************************
// File Name :         BossTowerBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Creates the boss towers and handles their destruction
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTowerBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField] private float health=20;
    BossBehavior bb;
    private bool destroyed;
    #endregion

    #region Functions

    //Handles references and checks
    #region Set Up
    /// <summary>
    /// Start is called before the first frame. Gets a reference to the boss. 
    /// </summary>
    void Start()
    {
        bb = GameObject.Find("Grayboxed Boss(Clone)").GetComponent<BossBehavior>();
    }

    /// <summary>
    /// Occurs every frame. Checks for tower death. 
    /// </summary>
    void Update()
    {
        //If health is 0 or less, remove itself from the list
        if (health <= 0)
        {
            bb.RemoveTower(this.gameObject);
        }
    }


    /// <summary>
    /// Handles collisions with objects
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If hit by bullet
        if (collision.gameObject.tag == "bullet")
        {
            //If hit by pistol bullet, take pistol damage
            if (collision.name.Contains("Pistol"))
            {
                PistolBulletBehavior pbb =
                    collision.gameObject.GetComponent<PistolBulletBehavior>();
                health -= pbb.damageDealt;

            }

            //If hit by revolver bullet, take revolver damage
            if (collision.name.Contains("Revolver"))
            {
                SheriffBulletBehavior sbb =
                    collision.gameObject.GetComponent<SheriffBulletBehavior>();
                health -= sbb.damageDealt;
            }

            //If hit by shotgun bullet/spray shotgun bullet, take shotgun damage
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

            //if health is 0 or less and not destroyed, destroy
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
            }
        }

        //Handles collisions with player explosions
        if (collision.gameObject.tag == "explodey")
        {
            //If hit by molotov, take fire damage
            if (collision.name.Contains("Fire"))
            {
                FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();
                health -= fb.damageDealt;
            }

            //otherwise take dynamite/firecracker damage
            else if (collision.name.Contains("Kaboom"))
            {
                DamageStoreExplodeBehavior dseb = collision.gameObject.
                    GetComponent<DamageStoreExplodeBehavior>();
                health -= dseb.damageDealt;
            }

            //if health is 0 or less and not destroyed, destroy
            if (health <= 0 && !destroyed)
            {
                destroyed = true;
            }
        }

    }
    #endregion
    #endregion
}
