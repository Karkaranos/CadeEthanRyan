/*****************************************************************************
// File Name :         WeaponData.cs
// Author :            Cade R. Naylor
// Creation Date :     March 21, 2023
//
// Brief Description : Creates Scriptable Objects for Weapons
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    #region Variables
    public enum WeaponTypeID {  GUN, EXPLOSIVE, MELEE}
    public enum WeaponID {  REVOLVER, SHOTGUN, PISTOL, DYNAMITE, COCKTAILS, 
        FIRECRACKERS, LASSO, KNUCKLES, FISTS }

    [SerializeField]
    private string weaponName;
    [SerializeField]
    private WeaponID weapon;
    [SerializeField]
    private WeaponTypeID weaponType;
    [SerializeField]
    private int dmg;
    [SerializeField]
    private float chargeDmg;
    [SerializeField]
    private float chargeCD;
    [SerializeField]
    private float standardCD;
    [SerializeField]
    private int ammo;
    [SerializeField]
    private int maxAmmo;

    public string WeaponName { get => weaponName; set => weaponName = value; }
    public WeaponID Weapon { get => weapon; set => weapon = value; }
    public WeaponTypeID WeaponType { get => weaponType; set => weaponType = value; }
    public int Dmg { get => dmg; set => dmg = value; }
    public float ChargeDmg { get => chargeDmg; set => chargeDmg = value; }
    public float ChargeCD { get => chargeCD; set => chargeCD = value; }
    public int Ammo { get => ammo; set => ammo = value; }
    public float StandardCD { get => standardCD; set => standardCD = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    #endregion
}
