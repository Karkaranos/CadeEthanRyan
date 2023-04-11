/*****************************************************************************
// File Name :         Player1UIBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 11, 2023
//
// Brief Description : Links player 1 with their UI
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1UIBehavior : MonoBehaviour
{
    [SerializeField] SheriffBehavior player1;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider ammoBar;
    [SerializeField] Sprite pistol;
    [SerializeField] Sprite revolver;
    [SerializeField] Sprite shotgun;
    [SerializeField] Image currWeapon;
    private float playerMaxHealth;
    private float playerMaxAmmo;


    private void Start()
    {
        player1 = GameObject.Find("Grayboxed Sheriff").
            GetComponent<SheriffBehavior>();
        playerMaxHealth = player1.Playerhealth;
        playerMaxAmmo = player1.MaxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.value = (player1.Playerhealth/playerMaxHealth);
        ammoBar.value = (player1.Ammo / playerMaxAmmo);
        if (player1.Weaponchanged)
        {
            playerMaxAmmo = player1.MaxAmmo;
            ammoBar.value = (player1.Ammo / playerMaxAmmo);
        }
        var img = currWeapon.GetComponent<Image>();
        if (player1.WeaponNumber == 1)
        {
            img.sprite = revolver;
        }
        if (player1.WeaponNumber == 2)
        {
            img.sprite = shotgun;
        }
        if (player1.WeaponNumber == 3)
        {
            img.sprite = pistol;
        }
    }
}
