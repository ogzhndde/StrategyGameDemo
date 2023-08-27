using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Scriptable object that holds data of buildings
/// </summary>

[CreateAssetMenu(fileName = "Fence", menuName = "Buildings/FenceData", order = 1)]
public class FenceSO : ScriptableObject
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
        Name = "Fence";
        Health = 30;
        CellSize = 11;
        BuildingType = BuildingType.Fence;
    }
}
