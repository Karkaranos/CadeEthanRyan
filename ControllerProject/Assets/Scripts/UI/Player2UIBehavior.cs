/*****************************************************************************
// File Name :         Player2UIBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 26, 2023
//
// Brief Description : Links player 1 with their UI
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2UIBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField] BanditBehavior player2;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider ammoBar;
    [SerializeField] Sprite cocktail;
    [SerializeField] Sprite dynamite;
    [SerializeField] Sprite firecracker;
    [SerializeField] Image currWeapon;
    private float playerMaxHealth;
    private float playerMaxAmmo;
    GameController gc;
    GameObject player2Obj;
    #endregion

    #region Functions

    /// <summary>
    /// Start is called before the first frame. Starts looking for Player 2
    /// </summary>
    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }

    /// <summary>
    /// Checks if Player 2 exists
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckForPlayers()
    {
        for (; ; )
        {
            //If player 2 doesn't exist, find it
            if (player2Obj == null)
            {
                player2Obj = GameObject.Find("Grayboxed Bandit(Clone)");
            }

            //If player 2 is found, stop checking and start updating UI
            if (player2Obj != null)
            {
                gc = GameObject.Find("Game Controller").GetComponent
                    <GameController>();
                player2 = GameObject.Find("Grayboxed Bandit(Clone)").
                    GetComponent<BanditBehavior>();
                playerMaxHealth = player2.Playerhealth;
                playerMaxAmmo = player2.MaxAmmo;
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }


    /// <summary>
    /// Called every frame. Updates UI
    /// </summary>
    void Update()
    {
        //If player 2 exists, update UI
        if (player2Obj != null)
        {
            //Set health and ammo bars
            healthBar.value = (player2.Playerhealth / playerMaxHealth);
            ammoBar.value = (player2.Ammo / playerMaxAmmo);

            //If the weapon has changed, set new max ammo
            if (player2.Weaponchanged)
            {
                playerMaxAmmo = player2.MaxAmmo;
                ammoBar.value = (player2.Ammo / playerMaxAmmo);
            }

            //Changes the weapon image to the current weapon
            var img = currWeapon.GetComponent<Image>();

            //If weapon is dynamite
            if (player2.WeaponNumber == 1)
            {
                img.sprite = dynamite;
            }

            //If weapon is cocktail
            if (player2.WeaponNumber == 2)
            {
                img.sprite = cocktail;
            }

            //If weapon is firecracker
            if (player2.WeaponNumber == 3)
            {
                img.sprite = firecracker;
            }
        }
    }
    #endregion
}
