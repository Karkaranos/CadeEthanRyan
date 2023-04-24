/*****************************************************************************
// File Name :         IncreaseStat.cs
// Author :            Cade R. Naylor
// Creation Date :     April 18, 2023
//
// Brief Description : Spawns refills for ammo and health
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStat : MonoBehaviour
{
    #region Variables
    SheriffBehavior sb;
    BanditBehavior bb;
    [Range(5, 20)]
    [SerializeField] private int healthAdded;
    [Range(5, 20)]
    [SerializeField] private int ammoAdded;
    private bool valAdded;
    #endregion

    #region Functions

    /// <summary>
    /// Start is called before the first frame. Gets reference to player.
    /// </summary>
    void Awake()
    {
        sb = GameObject.Find("Grayboxed Sheriff(Clone)").GetComponent<SheriffBehavior>();
        bb = GameObject.Find("Grayboxed Bandit(Clone)").GetComponent<BanditBehavior>();
    }

    /// <summary>
    /// Increases a player's stat if they collide with the stat item based on its 
    /// name
    /// </summary>
    /// <param name="collision">The object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (collision.gameObject.name.Contains("Bandit"))
            {
                if (name.Contains("heart") && !valAdded)
                {
                    sb.Playerhealth += healthAdded;
                    print(sb.Playerhealth);
                    valAdded = true;
                }
                if (name.Contains("ammo") && !valAdded)
                {
                    sb.Ammo += ammoAdded;
                    print(sb.Ammo);
                    valAdded = true;
                    if (sb.Ammo > sb.MaxAmmo)
                    {
                        sb.Ammo = sb.MaxAmmo;
                    }
                }
                if (name.Contains("cell"))
                {
                    sb.Cells++;
                }
            }
            if (collision.gameObject.name.Contains("Sheriff"))
            {
                if (name.Contains("heart") && !valAdded)
                {
                    bb.Playerhealth += healthAdded;
                    print(bb.Playerhealth);
                    valAdded = true;
                }
                if (name.Contains("ammo") && !valAdded)
                {
                    bb.Ammo += ammoAdded;
                    print(bb.Ammo);
                    valAdded = true;
                    if (bb.Ammo > bb.MaxAmmo)
                    {
                        bb.Ammo = bb.MaxAmmo;
                    }
                }
                if (name.Contains("cell"))
                {
                    bb.Cells++;
                }
            }
            Destroy(gameObject);
        }
    }
    #endregion
}
