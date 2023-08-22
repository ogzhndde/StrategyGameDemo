using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour, ISoldier
{
    [SerializeField] private string _name;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _cellSize;
    [SerializeField] private SoldierType _soldierType;
    [SerializeField] private TeamTypes _teamTypes;

    #region Interface Variables
    public string Name => _name;
    public int Health => _health;
    public int Damage => _damage;
    public int CellSize => _cellSize;
    public SoldierType SoldierType => _soldierType;
    #endregion

    public void SetSoldierProperties(string name, int health, int damage, int cellSize, SoldierType soldierType, TeamTypes teamTypes)
    {
        _name = name;
        _health = health;
        _damage = damage;
        _cellSize = cellSize;
        _soldierType = soldierType;
        _teamTypes = teamTypes;
    }


}
