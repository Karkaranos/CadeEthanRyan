using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject conScreen;
    public GameObject first;
    public GameObject second;
    
   
    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void HowToPlay()
    {
        titleScreen.SetActive(false);
        conScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(first);
    }

    public void BackButton()
    {
        titleScreen.SetActive(true);
        conScreen.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>()
            .SetSelectedGameObject(second);
    }
   
}
