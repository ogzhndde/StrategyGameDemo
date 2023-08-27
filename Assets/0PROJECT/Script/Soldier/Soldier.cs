using System;
using UnityEngine;

/// <summary>
/// The base class where the data of the soldiers is kept. 
/// All variables are assigned at the factory and this class passes this data to the classes it is responsible for.
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SoldierAI))]
[RequireComponent(typeof(DamageController))]
public class Soldier : MonoBehaviour, ISoldier
{
    [Header("Definitions")]
    private SoldierAI soldierAI;
    private DamageController damageController;


    [Space(15)]
    #region Main Variables
    [SerializeField] private string _name;
    [SerializeField] private Sprite _soldierSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _cellSize;
    [SerializeField] private SoldierType _soldierType;
    [SerializeField] private Color _soldierColor;
    public TeamTypes _teamTypes;
    #endregion

    #region Interface Variables
    public string Name => _name;
    public Sprite SoldierSprite => _soldierSprite;
    public int Health => _health;
    public int Damage => _damage;
    public int CellSize => _cellSize;
    public SoldierType SoldierType => _soldierType;
    #endregion

    //Set soldier properties when he/she created
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

        SetValuesOfSubScripts();
    }

    void Awake()
    {
        soldierAI = GetComponent<SoldierAI>();
        damageController = GetComponent<DamageController>();
    }

    //Send required data to subclasses
    private void SetValuesOfSubScripts()
    {
        damageController.SetHealthBarValues(_health, _soldierColor);
        soldierAI.teamTypes = _teamTypes;
    }

   
}
