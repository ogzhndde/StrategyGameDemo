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
        if (Input.GetKeyDown(KeyCode.A))
        {
            ArmyFactory.CreateSoldier(SoldierType.Rookie, TeamTypes.Blue, transform);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ArmyFactory.CreateSoldier(SoldierType.Officer, TeamTypes.Red, transform);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ArmyFactory.CreateSoldier(SoldierType.General, TeamTypes.Green, transform);
        }
        if (Input.GetMouseButtonDown(0))
        {
            BuildingFactory.SpawnForPlacement(BuildingType.Barrack, TeamTypes.Blue);
        }
        if (Input.GetMouseButtonDown(1))
        {
            BuildingFactory.SpawnForPlacement(BuildingType.PowerPlant, TeamTypes.Red);
        }
        if (Input.GetMouseButtonDown(2))
        {
            BuildingFactory.SpawnForPlacement(BuildingType.House, TeamTypes.Green);
        }


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
