using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class UIManager : SingletonManager<UIManager>
{
    [Inject]
    GameManager manager;
    
    GameData data;


    private void Start()
    {

    }

    void Update()
    {
        
    }

    //######################################################### BUTTONS ##############################################################

    void RandomButton()
    {

    }

    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        // EventManager.AddHandler(GameEvent.OnStart, OnStart);
    }

    private void OnDisable()
    {
        // EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
    }




}
