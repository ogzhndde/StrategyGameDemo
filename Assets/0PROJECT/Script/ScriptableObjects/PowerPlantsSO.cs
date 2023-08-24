using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerPlant", menuName = "Buildings/Power Plant Data", order = 1)]
public class PowerPlantsSO : ScriptableObject
{
    public string Name;
    [TextArea(4, 4)]
    public string Description;
    public Sprite BuildingSprite;
    public int Health;
    public int CellSize;
    public BuildingType BuildingType;

    public GameObject UnitPrefab;
    public GameObject ProductionMenuPrefab;

    [Button]
    public void ResetValues()
    {
        Name = "PowerPlant";
        Health = 50;
        CellSize = 23;
        BuildingType = BuildingType.PowerPlant;
    }
}
