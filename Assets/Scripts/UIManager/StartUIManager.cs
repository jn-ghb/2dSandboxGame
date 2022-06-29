using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using System;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameManager;
    [SerializeField]
    GameObject StartButton;
    [SerializeField]
    GameObject EndButton;


    GameManager _gameManager;


    void Awake()
    {
        _gameManager = GameManager.GetComponent<GameManager>();
    }


    public void SetForcusStartButton()
    {
        EventSystem.current.SetSelectedGameObject(StartButton);
    }

    public void ClickEvents()
    {
        if (EventSystem.current.currentSelectedGameObject == null) return;


        if (EventSystem.current.currentSelectedGameObject.tag == "Button_Start")
        {
            SceneManager.LoadScene("Main");
        }
        else if (EventSystem.current.currentSelectedGameObject.tag == "Button_End")
        {
            _gameManager.Quit();
        }
    }
}
