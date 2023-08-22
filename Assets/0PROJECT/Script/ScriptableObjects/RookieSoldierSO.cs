using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "RookieSoldierData", menuName = "Soldiers/Rookie Soldier Data", order = 1)]
public class RookieSoldierSO : ScriptableObject
{
    public GameObject soldierPrefab;
    public string Name = "Rookie";
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
