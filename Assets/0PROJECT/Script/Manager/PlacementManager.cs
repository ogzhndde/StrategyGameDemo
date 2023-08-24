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
    private Vector3 tileBottomLeft;

    void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(cellPos);
        tileBottomLeft = cellWorldPos - new Vector3(tilemap.cellSize.x / 2f, tilemap.cellSize.y / 2f, 0f);

        currentTile = tilemap.GetTile(cellPos);

        //SET POSITION OF HIGHLIGHTER SPRITE
        GetCurrentMovingObject().transform.position = tileBottomLeft;

        MouseOperations();
    }

    private void MouseOperations()
    {
        if (currentTile == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            PlaceAnyBuilding();
        }
    }


    private void PlaceAnyBuilding()
    {
        if (SelectedBuilding == null)
            return;

        Instantiate(SelectedBuilding, tileBottomLeft, Quaternion.identity);
    }

    private GameObject GetCurrentMovingObject()
    {
        highlighter.gameObject.SetActive(SelectedBuilding == null ? true : false);

        if (SelectedBuilding == null)
            return highlighter;
        else
            return SelectedBuilding;
    }





    //##########################        EVENTS      ###################################

    void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnSelectBuilding, OnSelectBuilding);
        EventManager.AddHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnSelectBuilding, OnSelectBuilding);
        EventManager.RemoveHandler(GameEvent.OnPlaceBuilding, OnPlaceBuilding);
    }

    private void OnSelectBuilding(object _selectedBuilding, object _buildingType, object _teamType)
    {
        GameObject building = (GameObject)_selectedBuilding;
        BuildingType buildingType = (BuildingType)_buildingType;
        TeamTypes teamType = (TeamTypes)_teamType;
    }
    private void OnPlaceBuilding()
    {

    }
}
