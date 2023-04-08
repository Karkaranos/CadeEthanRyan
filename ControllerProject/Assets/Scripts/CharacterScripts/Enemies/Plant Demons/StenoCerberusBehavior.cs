/*****************************************************************************
// File Name :         StenoCerberusBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     April 8, 2023
//
// Brief Description : Handles attacks and death conditions for StenoCerberus
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StenoCerberusBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField] private float rateFired;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject atkPoint1;
    [SerializeField] private GameObject atkPoint2;
    [SerializeField] private GameObject atkPoint3;

    //References to players and setting targets
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;

    #endregion Variables

    #region Functions


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Functions
}
