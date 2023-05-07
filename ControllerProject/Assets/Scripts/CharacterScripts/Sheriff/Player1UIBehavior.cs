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

    //Updates the UI and checks for players
    #region Functions

    /// <summary>
    /// Occurs before the first frame; starts looking for players
    /// </summary>
    void Start()
    {
        StartCoroutine(CheckForPlayers());
    }

    /// <summary>
    /// Repeats until it finds Player 1
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckForPlayers()
    {
        for (; ; )
        {
            //If Player 1 does not exist, find it
            if (player1Obj == null)
            {
                player1Obj = GameObject.Find("Grayboxed Sheriff(Clone)");
            }

            //If Player 1 is found, get its behavior
            if (player1Obj != null)
            {
                print("found");
                gc = GameObject.Find("Game Controller").GetComponent<GameController>
                    ();
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
        //If Player 1 exists, update their UI
        if (player1Obj != null)
        {
            //Set the health and ammo bars to their current values
            healthBar.value = (player1.Playerhealth / playerMaxHealth);
            ammoBar.value = (player1.Ammo / playerMaxAmmo);

            //If the weapon was changed, set the new max ammo
            if (player1.Weaponchanged)
            {
                playerMaxAmmo = player1.MaxAmmo;
                ammoBar.value = (player1.Ammo / playerMaxAmmo);
            }

            //Change the current weapon's image to match
            var img = currWeapon.GetComponent<Image>();

            //If player is using Revolver, set image to revolver
            if (player1.WeaponNumber == 1)
            {
                img.sprite = revolver;
            }

            //If player is using shotgun, set image to shotgun
            if (player1.WeaponNumber == 2)
            {
                img.sprite = shotgun;
            }

            //If player is using pistol, set image to pistol
            if (player1.WeaponNumber == 3)
            {
                img.sprite = pistol;
            }

            //Update wave and enemy counter
            enemyCount.text = "Enemies Left: " + gc.enemyCounter;
            waveCount.text = "Wave: " + gc.wave;
        }
    }
    #endregion
}
