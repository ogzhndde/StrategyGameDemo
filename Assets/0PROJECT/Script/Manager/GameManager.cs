using System;
using ArmyFactoryStatic;
using BuildingFactoryStatic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : SingletonManager<GameManager>
{

    public ScriptableObjects SO;


    [Serializable]
    public struct ScriptableObjects
    {
        public GameData data;
        public RookieSoldierSO rookieData;
        public OfficerSoldierSO officerData;
        public GeneralSoldierSO GeneralData;
        public BarrackSO BarrackData;
        public PowerPlantsSO PowerPlantData;
        public HouseSO HouseData;
        public FenceSO FenceData;
    }


    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
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
