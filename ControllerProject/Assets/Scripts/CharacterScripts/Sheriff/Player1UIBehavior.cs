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
    #region Variables
    [SerializeField] SheriffBehavior player1;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider ammoBar;
    [SerializeField] Sprite pistol;
    [SerializeField] Sprite revolver;
    [SerializeField] Sprite shotgun;
    [SerializeField] Image currWeapon;
    [SerializeField] Text enemyCount;
    [SerializeField] Text waveCount;
    private float playerMaxHealth;
    private float playerMaxAmmo;
    GameController gc;
    GameObject player1Obj;
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
            if (player1Obj == null)
            {
                player1Obj = GameObject.Find("Grayboxed Sheriff(Clone)");
            }
            if (player1Obj != null)
            {
                gc = GameObject.Find("Game Controller").GetComponent<GameController>();
                player1 = GameObject.Find("Grayboxed Sheriff(Clone)").
                    GetComponent<SheriffBehavior>();
                playerMaxHealth = player1.Playerhealth;
                playerMaxAmmo = player1.MaxAmmo;
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
        if (player1Obj != null)
        {
            healthBar.value = (player1.Playerhealth / playerMaxHealth);
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
            enemyCount.text = "Enemies Left: " + gc.enemyCounter;
            waveCount.text = "Wave: " + gc.wave;
        }
    }
    #endregion
}
