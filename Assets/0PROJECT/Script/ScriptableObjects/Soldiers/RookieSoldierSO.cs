using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Scriptable object that holds data of soldiers
/// </summary>

[CreateAssetMenu(fileName = "RookieSoldierData", menuName = "Soldiers/Rookie Soldier Data", order = 1)]
public class RookieSoldierSO : ScriptableObject
{
    public GameObject soldierPrefab;
    public GameObject soldierUIPrefab;
    public string Name = "Rookie";
    public Sprite soldierSprite;
    public int health = 10;
    public int damage = 2;
    public int cellSize = 11;
    public SoldierType soldierType = SoldierType.Rookie;

    [Button]
    public void ResetValues()
    {
        Name = "Rookie";
        health = 10;
        damage = 2;
        cellSize = 11;
        soldierType = SoldierType.Rookie;
    }
}
