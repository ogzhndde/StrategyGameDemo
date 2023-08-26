using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CombatController))]
public class Soldier : MonoBehaviour, ISoldier
{
    [Header("Definitions")]
    [SerializeField] private CombatController combatController;


    [Space(15)]
    #region Main Variables
    [SerializeField] private string _name;
    [SerializeField] private Sprite _soldierSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _cellSize;
    [SerializeField] private SoldierType _soldierType;
    [SerializeField] private TeamTypes _teamTypes;
    [SerializeField] private Color _soldierColor;
    #endregion

    #region Interface Variables
    public string Name => _name;
    public Sprite SoldierSprite => _soldierSprite;
    public int Health => _health;
    public int Damage => _damage;
    public int CellSize => _cellSize;
    public SoldierType SoldierType => _soldierType;

    #endregion

    public void SetSoldierProperties(string name, Sprite soldierSprite, int health, int damage, int cellSize, SoldierType soldierType, TeamTypes teamTypes)
    {
        _name = name;
        _soldierSprite = soldierSprite;
        _health = health;
        _damage = damage;
        _cellSize = cellSize;
        _soldierType = soldierType;
        _teamTypes = teamTypes;
        _soldierColor = teamTypes switch
        {
            TeamTypes.Blue => Color.blue,
            TeamTypes.Red => Color.red,
            TeamTypes.Green => Color.green,
            _ => Color.white,
        };

        SetCombatValues();
    }

    private void SetCombatValues()
    {
        combatController.SetHealthBarValues(_health, _soldierColor);
    }





}
