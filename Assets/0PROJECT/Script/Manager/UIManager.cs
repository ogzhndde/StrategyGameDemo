using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : SingletonManager<UIManager>
{
    public GameManager manager;
    public GameData data;

    //[Header("Definitions")]
    public TextMeshProUGUI LevelText;


    private void Start()
    {
        manager = GameManager.Instance;

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
