/*****************************************************************************
// File Name :         ButtonBehaviour.cs
// Author :            Ethan S. Reising
// Creation Date :     April 13, 2023
//
// Brief Description : Manages UI/Menu buttons
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject firstScreen;
    public GameObject secondScreen;
    public GameObject thirdScreen;
    public GameObject first;
    public GameObject second;
    
   
    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void HowToPlay()
    {
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(first);
    }

    public void BackButton()
    {
        firstScreen.SetActive(true);
        secondScreen.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(second);
    }

    public void ResumeButton()
    {
        firstScreen.SetActive(true);
        secondScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
       
    }
   
    public void Settings()
    {
        secondScreen.SetActive(false);
        thirdScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(second);
    }

    public void PauseBack()
    {
        thirdScreen.SetActive(false);
        secondScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(first);
    }
}
