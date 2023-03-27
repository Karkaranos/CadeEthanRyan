using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public enum CharacterType { SHERIFF, BANDIT, GAMBLER }

    [SerializeField]
    private string characterName;
    [SerializeField]
    private CharacterType character;
    [SerializeField]
    private int health;
    [SerializeField]
    private int demonicPossession;
    [SerializeField]
    private int critDmgPercent;

    public string CharacterName { get => characterName; set => characterName = value; }
    public CharacterType Character { get => character; set => character = value; }
    public int Health { get => health; set => health = value; }
    public int DemonicPossession { get => demonicPossession; set => demonicPossession = value; }
}
