using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour, ISoldier
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _soldierSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _cellSize;
    [SerializeField] private SoldierType _soldierType;
    [SerializeField] private TeamTypes _teamTypes;

    #region Interface Variables
    public string Name => _name;
    public Sprite SoldierSprite => _soldierSprite;
    public int Health => _health;
    public int Damage => _damage;
    public int CellSize => _cellSize;
    public SoldierType SoldierType => _soldierType;

    #endregion

    public void SetSoldierProperties(string name,Sprite soldierSprite, int health, int damage, int cellSize, SoldierType soldierType, TeamTypes teamTypes)
    {
        _name = name;
        _soldierSprite = soldierSprite;
        _health = health;
        _damage = damage;
        _cellSize = cellSize;
        _soldierType = soldierType;
        _teamTypes = teamTypes;
    }


}
