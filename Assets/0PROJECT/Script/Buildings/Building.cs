using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main building class. It is found by default in every building that is produced. 
/// It stores all the necessary information in it.
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(DamageController))]
public class Building : MonoBehaviour, IBuilding
{

    [Space(15)]
    [Header("Definitons")]
    [SerializeField] private SpriteRenderer SPR_MainBuilding;
    [SerializeField] private SpriteRenderer SPR_TeamFlag;
    private DamageController damageController;
    private BoxCollider2D coll;
    public Color CLR_BuildingColor;


    [Space(10)]
    [Header("Control Bools")]
    public bool _canBuildingPlace = true;
    public bool _isPlaced = false;
    [SerializeField] private LayerMask SpawnDetectionLayerMask;


    #region Main Variables
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _buildingSprite;
    [SerializeField] private Sprite _buildingInformationSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _cellSize;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private List<SoldierType> _buildingUnits;
    public TeamTypes _teamTypes;
    public List<Transform> _unitSpawnPoints;
    #endregion

    #region Interface Variables
    public string Name => _name;
    public string Description => _description;
    public Sprite BuildingSprite => _buildingSprite;
    public Sprite BuildingInformationSprite => _buildingInformationSprite;
    public int Health => _health;
    public int CellSize => _cellSize;
    public BuildingType BuildingType => _buildingType;
    public List<SoldierType> BuildingUnits => _buildingUnits;
    #endregion

    //When the building is created, it assigns the all properties.
    public void SetBuildingProperties(string name, string description, Sprite buildingSprite, Sprite buildingInformationSprite, int health, int cellSize, BuildingType buildingType, TeamTypes teamTypes, List<SoldierType> buildingUnits = null)
    {
        _name = name;
        _description = description;
        _buildingSprite = buildingSprite;
        _buildingInformationSprite = buildingInformationSprite;
        _health = health;
        _cellSize = cellSize;
        _buildingType = buildingType;
        _teamTypes = teamTypes;
        _buildingUnits = buildingUnits;
    }

    //Sets the building's visual assignments.
    public void SetVisualProperties()
    {
        CLR_BuildingColor = _teamTypes switch
        {
            TeamTypes.Red => Color.red,
            TeamTypes.Blue => Color.blue,
            TeamTypes.Green => Color.green,
            _ => Color.white
        };
        SPR_TeamFlag.color = CLR_BuildingColor;

        SPR_MainBuilding.sortingLayerName = "Units";
        SPR_TeamFlag.sortingLayerName = "Units";

        SetValuesOfSubScripts();
    }

    // When the production button is pressed, it creates the building with the data from the factory.
    public void CreateBuilding()
    {
        EventManager.Broadcast(GameEvent.OnClickBuildingUI, gameObject, _buildingType, _teamTypes);
    }

    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        damageController = GetComponent<DamageController>();
    }

    void FixedUpdate()
    {
        CheckCollision();
    }

    // Sends an event for the UI check when clicking on a placed building.
    public void ClickPlacedBuilding()
    {
        if (!_isPlaced) return;
        EventManager.Broadcast(GameEvent.OnClickPlacedBuilding, gameObject, _buildingType, _teamTypes);
        EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSelectBuilding");

    }

    // Checks if the building is in a suitable location while the building has not yet been placed.
    private void CheckCollision()
    {
        if (_isPlaced) return;

        RaycastHit2D[] AllCollisionObjects = Physics2D.BoxCastAll(coll.bounds.center, coll.bounds.size, 0f, Vector2.zero, 0f);

        //If building collide with any object except own collider
        switch (AllCollisionObjects.Length)
        {
            case > 1:
                _canBuildingPlace = false;
                CheckBuildingSprites(Color.red, .5f);
                break;
            default:
                _canBuildingPlace = true;
                CheckBuildingSprites(Color.white, 0.5f);
                break;
        }
    }

    //Set color and alpha value of main sprites of the building.
    private void CheckBuildingSprites(Color targetColor, float alphaValue)
    {
        Color newColor = targetColor;
        newColor.a = alphaValue;
        SPR_MainBuilding.color = newColor;

        Color flagColor = CLR_BuildingColor;
        flagColor.a = alphaValue;
        SPR_TeamFlag.color = flagColor;
    }

    public bool CheckBuildingPlaceability()
    {
        return _canBuildingPlace;
    }

    // Makes appropriate assignments for subscripts.
    private void SetValuesOfSubScripts()
    {
        damageController.SetHealthBarValues(_health, CLR_BuildingColor);
    }

    // Since the object is created by pooling, it reassigns variables on close-open states.
    private void BuildingVariablesCheck(bool isPlaced, string sortingLayer, bool isTrigger, Color color, float opacity)
    {
        _isPlaced = isPlaced;
        SPR_MainBuilding.sortingLayerName = sortingLayer;
        SPR_TeamFlag.sortingLayerName = sortingLayer;

        coll.isTrigger = isTrigger;
        CheckBuildingSprites(color, opacity);
    }

    // Returns the appropriate location around the building where it can spawn soldiers.
    public Transform FindValidSpawnPoint()
    {
        foreach (Transform spawnPoint in _unitSpawnPoints)
        {
            Collider2D overlap = Physics2D.OverlapCircle(spawnPoint.position, 0.16f, SpawnDetectionLayerMask);

            if (overlap == null)
            {
                return spawnPoint;
            }
        }
        return _unitSpawnPoints[0]; // Return null if no valid spawn point is found
    }



    //##########################        EVENTS      ###################################
    void OnEnable()
    {
        //Add using events on class
        EventManager.AddHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    void OnDisable()
    {
        //Remove using events on class 
        EventManager.RemoveHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);

        //When destroy the building and send pool, reset the values
        BuildingVariablesCheck(
                isPlaced: false,
                sortingLayer: "Units",
                isTrigger: true,
                color: Color.white,
                opacity: 1f);
    }

    private void OnPlaceBuilding(object value)
    {
        GameObject selectedBuilding = (GameObject)value;

        if (selectedBuilding == gameObject)
        {
            BuildingVariablesCheck(
                isPlaced: true,
                sortingLayer: "PlacedUnits",
                isTrigger: false,
                color: Color.white,
                opacity: 1f);
        }
    }

}
