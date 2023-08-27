using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementManager : SingletonManager<PlacementManager>
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase currentTile;
    [SerializeField] private GameObject SelectedBuilding;

    [SerializeField] private GameObject highlighter;
    public Vector3 cellWorldPos;
    private Vector3 tileBottomLeft;

    void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

        cellWorldPos = tilemap.GetCellCenterWorld(cellPos);
        tileBottomLeft = cellWorldPos - new Vector3(tilemap.cellSize.x / 2f, tilemap.cellSize.y / 2f, 0f);

        currentTile = tilemap.GetTile(cellPos);

        //SET POSITION OF HIGHLIGHTER SPRITE
        GetCurrentMovingObject().transform.position = tileBottomLeft;

        MouseOperations();
    }

    private void MouseOperations()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceAnyBuilding();
        }
    }


    private void PlaceAnyBuilding()
    {
        if (SelectedBuilding == null) return;

        Building building = SelectedBuilding.GetComponent<Building>();
        if (!building.CheckBuildingPlaceability()) return;

        EventManager.Broadcast(GameEvent.OnPlaceBuilding, SelectedBuilding);
        EventManager.Broadcast(GameEvent.OnPlaySound, "SoundPlacement");

    }

    private GameObject GetCurrentMovingObject()
    {
        highlighter.gameObject.SetActive(SelectedBuilding == null ? true : false);

        if (SelectedBuilding == null)
            return highlighter;
        else
            return SelectedBuilding;
    }

    private void ClearPreviousBuilding()
    {
        if (SelectedBuilding == null) return;

        ObjectPoolManager.ReturnObjectToPool(SelectedBuilding);
    }





    //##########################        EVENTS      ###################################

    void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnClickBuildingUI, OnClickBuildingUI);
        EventManager.AddHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnClickBuildingUI, OnClickBuildingUI);
        EventManager.RemoveHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    private void OnClickBuildingUI(object _selectedBuilding, object _buildingType, object _teamType)
    {
        GameObject building = (GameObject)_selectedBuilding;

        ClearPreviousBuilding();

        SelectedBuilding = building;
    }
    private void OnPlaceBuilding(object value)
    {
        SelectedBuilding = null;
    }
}
