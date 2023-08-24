using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour, IBuilding
{
    [Header("Definitons")]
    [SerializeField] private SpriteRenderer SPR_TeamFlag;
    [SerializeField] private BoxCollider2D coll;

    [Space(10)]
    [Header("Definitons")]
    public bool _canBuildingPlace = true;
    public bool _isPlaced = false;


    #region Main Variables
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _buildingSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _cellSize;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private TeamTypes _teamTypes;
    [SerializeField] private List<GameObject> _buildingUnits;
    [SerializeField] private Transform _unitSpawnPoint;
    #endregion

    #region Interface Variables
    public string Name => _name;
    public string Description => _description;
    public Sprite BuildingSprite => _buildingSprite;
    public int Health => _health;
    public int CellSize => _cellSize;
    public BuildingType BuildingType => _buildingType;
    public List<GameObject> BuildingUnits => _buildingUnits;


    #endregion
    public void SetBuildingProperties(string name, string description, Sprite buildingSprite, int health, int cellSize, BuildingType buildingType, TeamTypes teamTypes, List<GameObject> buildingUnits = null)
    {
        _name = name;
        _description = description;
        _buildingSprite = buildingSprite;
        _health = health;
        _cellSize = cellSize;
        _buildingType = buildingType;
        _teamTypes = teamTypes;
        _buildingUnits = buildingUnits;
    }

    public void SetVisualProperties()
    {
        this.name = _name + " " + _teamTypes.ToString();

        SPR_TeamFlag.color = _teamTypes switch
        {
            TeamTypes.Red => Color.red,
            TeamTypes.Blue => Color.blue,
            TeamTypes.Green => Color.green,
            _ => Color.white
        };
    }

    public void CreateBuilding()
    {
        EventManager.Broadcast(GameEvent.OnClickBuildingUI, gameObject, _buildingType, _teamTypes);
    }

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        CheckCollision();
    }


    void OnMouseDown()
    {
        if (!_isPlaced) return;

        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Broadcast(GameEvent.OnClickPlacedBuilding, gameObject, _buildingType, _teamTypes);
        }

        if (Input.GetMouseButtonDown(1))
        {

        }
    }

    private void CheckCollision()
    {
        if (_isPlaced) return;

        RaycastHit2D[] AllCollisionObjects = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size, 0f, Vector2.zero, 0f);
        Debug.Log(AllCollisionObjects[^1].collider.gameObject.name);
        
        //IF BUILDING COLLIDE WITH ANY OBJECT EXCEPT OWN COLLIDER
        _canBuildingPlace = AllCollisionObjects.Length > 1 ? false : true;
    }

    public bool CheckBuildingPlaceability()
    {
        return _canBuildingPlace;
    }

    //##########################        EVENTS      ###################################

    void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    private void OnPlaceBuilding(object value)
    {
        GameObject selectedBuilding = (GameObject)value;

        if (selectedBuilding == gameObject)
        {
            _isPlaced = true;
        }
    }


}
