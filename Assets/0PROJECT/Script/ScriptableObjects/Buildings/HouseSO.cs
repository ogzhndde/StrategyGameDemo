using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Scriptable object that holds data of buildings
/// </summary>

[CreateAssetMenu(fileName = "House", menuName = "Buildings/House Data", order = 1)]
public class HouseSO : ScriptableObject
{
    public string Name;
    [TextArea(4, 4)]
    public string Description;
    public Sprite BuildingSprite;
    public Sprite BuildingInformationSprite;
    public int Health;
    public int CellSize;
    public BuildingType BuildingType;

    public GameObject BuildingPrefab;
    public GameObject ProductionMenuPrefab;

    [Button]
    public void ResetValues()
    {
        Name = "House";
        Health = 60;
        CellSize = 22;
        BuildingType = BuildingType.House;
    }
}
