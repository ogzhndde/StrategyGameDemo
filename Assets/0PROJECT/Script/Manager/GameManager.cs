using System;
using UnityEngine;

/// <summary>
/// The class where the main variables related to the game are kept.
/// </summary>

public class GameManager : SingletonManager<GameManager>
{
    public ScriptableObjects SO;

    [Serializable]
    public struct ScriptableObjects
    {
        public RookieSoldierSO rookieData;
        public OfficerSoldierSO officerData;
        public GeneralSoldierSO GeneralData;
        public BarrackSO BarrackData;
        public PowerPlantsSO PowerPlantData;
        public HouseSO HouseData;
        public FenceSO FenceData;
        public ParticleSO ParticleData;
        public CursorSO CursorData;
    }


}
