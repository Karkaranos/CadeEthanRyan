/*****************************************************************************
// File Name :         UIManagerBehavior.cs
// Author :            Ethan Reising*
// Creation Date :     April 13, 2023
//
// Brief Description : UI Manager
                        *Code was originally added to Sheriff Behavior. Cade Naylor
                        *made it its own script, but changed no content.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIManagerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject firstScreen;
    [SerializeField] private GameObject secondScreen;
    [SerializeField] private GameObject firstSelected;

    /// <summary>
    /// Opens PauseMenu
    /// </summary>
    public void PauseMenu()
    {
        if (firstScreen == null)
        {

            firstScreen = GameObject.Find("PlayerUI 1");

}

        if (secondScreen == null)
        {

            secondScreen = GameObject.Find("PauseMenu");
        
}
        Time.timeScale = 0;
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(firstSelected);
    }
}
