using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    public enum WeaponType {  GUN, EXPLOSIVE, MELEE}
    public enum Weapon {  REVOLVER, SHOTGUN, PISTOL, DYNAMITE, COCKTAILS, 
        FIRECRACKERS, LASSO, KNUCKLES, FISTS }

    [SerializeField]
    private string weaponName;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private int dmg;
}
