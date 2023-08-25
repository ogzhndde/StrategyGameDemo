using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "OfficerSoldierData", menuName = "Soldiers/Officer Soldier Data", order = 1)]
public class OfficerSoldierSO : ScriptableObject
{
    public GameObject soldierPrefab;
    public GameObject soldierUIPrefab;
    public string Name = "Officer";
    public Sprite soldierSprite;
    public int health = 10;
    public int damage = 5;
    public int cellSize = 11;
    public SoldierType soldierType = SoldierType.Officer;


    [Button]
    public void ResetValues()
    {
        Name = "Officer";
        health = 10;
        damage = 5;
        cellSize = 1;
        soldierType = SoldierType.Officer;
    }
}