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


    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }

    IEnumerator CheckForPlayers()
    {
        for (; ; )
        {
            if (player2Obj == null)
            {
                player2Obj = GameObject.Find("Grayboxed Bandit(Clone)");
            }
            if (player2Obj != null)
            {
                gc = GameObject.Find("Game Controller").GetComponent<GameController>();
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
        if (player2Obj != null)
        {
            healthBar.value = (player2.Playerhealth / playerMaxHealth);
            ammoBar.value = (player2.Ammo / playerMaxAmmo);
            if (player2.Weaponchanged)
            {
                playerMaxAmmo = player2.MaxAmmo;
                ammoBar.value = (player2.Ammo / playerMaxAmmo);
            }
            var img = currWeapon.GetComponent<Image>();
            if (player2.WeaponNumber == 1)
            {
                img.sprite = dynamite;
            }
            if (player2.WeaponNumber == 2)
            {
                img.sprite = cocktail;
            }
            if (player2.WeaponNumber == 3)
            {
                img.sprite = firecracker;
            }
        }
    }
    #endregion
}
