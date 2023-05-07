/*****************************************************************************
// File Name :         TutorialManager.cs
// Author :            Ethan S. Reising
// Creation Date :     May 7, 2023
//
// Brief Description : Game Manager for the Tutorial level
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject bottle1, bottle2, bottle3;
    public List<GameObject> bottles = new List<GameObject>();
    private Vector2 spawnPos;
    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        GameObject test;
        for (int i=0; i < 5;)
        {
            spawnPos = new Vector2(Random.Range(-9,9), Random.Range(-5, 1));
            test = Instantiate(bottle1, spawnPos, Quaternion.identity);
            bottles.Add(test);
            i++;
        }
        for (int i = 0; i < 5;)
        {
            spawnPos = new Vector2(Random.Range(-9, 9), Random.Range(-5, 1));
            test = Instantiate(bottle2, spawnPos, Quaternion.identity);
            bottles.Add(test);
            i++;
        }
        for (int i = 0; i < 5;)
        {
            spawnPos = new Vector2(Random.Range(-9, 9), Random.Range(-5, 1));
            test = Instantiate(bottle3, spawnPos, Quaternion.identity);
            bottles.Add(test);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(bottles.Count == 0)
        {
            door.SetActive(true);
        }
    }
}
