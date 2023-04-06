using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffBulletBehavior : MonoBehaviour
{
    [SerializeField] private GameObject scope;
    void Start()
    {
        scope = GameObject.Find("Scope");
        //Set direction
    }
}
