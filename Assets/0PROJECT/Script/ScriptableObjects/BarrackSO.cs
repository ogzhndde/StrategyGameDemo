using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrack", menuName = "Buildings/Barrack Data", order = 1)]
public class BarrackSO : ScriptableObject
{
    public string Name;
    [TextArea(4, 4)]
    public string Description;
    public int Health;
    public int CellSize;
    public BuildingType BuildingType;

    public GameObject UnitPrefab;
    public GameObject ProductionMenuPrefab;
    public List<GameObject> UnitPrefabs;

    [Button]
    public void ResetValues()
    {
        Name = "Barrack";
        Health = 100;
        CellSize = 44;
        BuildingType = BuildingType.Barrack;
    }
}
