/*****************************************************************************
// File Name :         LootTableAndDropBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 23, 2023
//
// Brief Description : Handles summoning loot upon enemy death
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTableAndDropBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject cell;
    [SerializeField] GameObject health;
    [SerializeField] GameObject ammo;

    private int randomNum;
    private int totalChances=1;
    private int totalPool=20;
    #endregion

    #region Functions

    /// <summary>
    /// Has the potential to spawn items at the position where an enemy died
    /// </summary>
    /// <param name="pos">the dead enemy's position</param>
    public void DropLoot(Vector2 pos)
    {
        GameController gc = GameObject.Find("Game Controller").
            GetComponent<GameController>();
        //Instantiate(cell, pos, Quaternion.identity);
        for(int i=0; i<totalChances; i++)
        {
            randomNum = Random.Range(1, totalPool+1);

            //Spawn a cell with a 1 in 20 chance
            if(randomNum ==1)
            {
                pos.x += Random.Range(-1, 1);
                pos.y += Random.Range(-1, 1);
                Instantiate(cell, pos, Quaternion.identity);
            }

            //Spawn health with a 3 in 20 chance if in the first half of waves
            if(randomNum >=5 && randomNum <= 7 && gc.wave <= 4)
            {
                pos.x += Random.Range(-1, 1);
                pos.y += Random.Range(-1, 1);
                Instantiate(health, pos, Quaternion.identity);
            }

            //Spawn health with a 2 in 5 chance if in the second half of waves
            if (randomNum >= 5 && randomNum <= 8 && gc.wave > 4)
            {
                pos.x += Random.Range(-1, 1);
                pos.y += Random.Range(-1, 1);
                Instantiate(health, pos, Quaternion.identity);
            }

            //Spawn ammo with a 3 in 20 chance 
            if (randomNum>=12 && randomNum <= 15)
            {
                pos.x += Random.Range(-1, 1);
                pos.y += Random.Range(-1, 1);
                Instantiate(ammo, pos, Quaternion.identity);
            }


        }
    }
    #endregion
}
