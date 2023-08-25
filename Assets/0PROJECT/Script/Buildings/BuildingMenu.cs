using System.Collections;
using System.Collections.Generic;
using BuildingFactoryStatic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour, IBuilding
{
    [Header("Definitons")]
    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private TextMeshProUGUI TMP_CellSize;
    [SerializeField] private Image IMA_Building;
    [SerializeField] private Image IMA_TeamFlag;
    [SerializeField] private Button BTN_BuildingUI;

    [Space(10)]
    #region Main Variables Of Building
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _buildingSprite;
    [SerializeField] private Sprite _buildingInformationSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _cellSize;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private TeamTypes _teamTypes;
    [SerializeField] private List<SoldierType> _buildingUnits;
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

    public void SetVisualProperties()
    {
        this.name = _name + " " + _teamTypes.ToString();

        TMP_Name.text = _name;
        TMP_CellSize.text = _cellSize.ToString()[0] + "x" + _cellSize.ToString()[1];
        IMA_Building.sprite = _buildingSprite;
        IMA_TeamFlag.color = _teamTypes switch
        {
            TeamTypes.Red => Color.red,
            TeamTypes.Blue => Color.blue,
            TeamTypes.Green => Color.green,
            _ => Color.white
        };
    }

    void Start()
    {
        BTN_BuildingUI.onClick.AddListener(ButtonBuilding);
    }

    private void ButtonBuilding()
    {
        BuildingFactory.SpawnForPlacement(_buildingType, _teamTypes);
    }

}
