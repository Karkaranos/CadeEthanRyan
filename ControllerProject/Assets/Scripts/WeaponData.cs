using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
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
    private int chargeDmg;

    public string WeaponName { get => weaponName; set => weaponName = value; }
    public WeaponID Weapon { get => weapon; set => weapon = value; }
    public WeaponTypeID WeaponType { get => weaponType; set => weaponType = value; }
    public int Dmg { get => dmg; set => dmg = value; }
}
