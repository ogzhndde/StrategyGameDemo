using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _health;
    [SerializeField] private int _cellSize;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private TeamTypes _teamTypes;
    [SerializeField] private List<GameObject> _buildingUnits;
    [SerializeField] private Transform _unitSpawnPoint;

    #region Interface Variables
    public string Name => _name;
    public string Description => _description;
    public int Health => _health;
    public int CellSize => _cellSize;
    public BuildingType BuildingType => _buildingType;
    public List<GameObject> BuildingUnits => _buildingUnits;

    #endregion
    public void SetBuildingProperties(string name, string description, int health, int cellSize, BuildingType buildingType, TeamTypes teamTypes, List<GameObject> buildingUnits = null)
    {
        _name = name;
        _description = description;
        _health = health;
        _cellSize = cellSize;
        _buildingType = buildingType;
        _teamTypes = teamTypes;
        _buildingUnits = buildingUnits;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Broadcast(GameEvent.OnClickPlacedBuilding, gameObject, _buildingType, _teamTypes);
        }

        if (Input.GetMouseButtonDown(1))
        {

        }
    }


}
