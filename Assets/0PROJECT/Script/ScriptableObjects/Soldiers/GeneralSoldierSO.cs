using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralSoldierData", menuName = "Soldiers/General Soldier Data", order = 1)]
public class GeneralSoldierSO : ScriptableObject
{
    public GameObject soldierPrefab;
    public GameObject soldierUIPrefab;
    public string Name = "General";
    public Sprite soldierSprite;
    public int health = 10;
    public int damage = 10;
    public int cellSize = 11;
    public SoldierType soldierType = SoldierType.General;

    [Button]
    public void ResetValues()
    {
        Name = "General";
        health = 10;
        damage = 10;
        cellSize = 11;
        soldierType = SoldierType.General;
    }
}