using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The class that controls the placement operations of the produced buildings. 
/// By reaching the tilemap system, it places in the gridal system.
/// </summary>

public class PlacementManager : SingletonManager<PlacementManager>
{
    [Header("Definitions")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject highlighter;

    [Space(15)]
    [Header("Conrtol Variables")]
    [SerializeField] private GameObject SelectedBuilding;
    [SerializeField] private TileBase currentTile;
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

        //Detect left-bottom point of grid to place building
        tileBottomLeft = cellWorldPos - new Vector3(tilemap.cellSize.x / 2f, tilemap.cellSize.y / 2f, 0f);

        currentTile = tilemap.GetTile(cellPos);

        //Set position of Highlighter object
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

    //If there is selected building and the buildings is in a available area, place the building
    private void PlaceAnyBuilding()
    {
        if (SelectedBuilding == null) return;

        Building building = SelectedBuilding.GetComponent<Building>();
        if (!building.CheckBuildingPlaceability()) return;

        EventManager.Broadcast(GameEvent.OnPlaceBuilding, SelectedBuilding);
        EventManager.Broadcast(GameEvent.OnPlaySound, "SoundPlacement");
    }


    //Return which object will follow the cursor, selectedBuilding or highlighter
    private GameObject GetCurrentMovingObject()
    {
        highlighter.gameObject.SetActive(SelectedBuilding == null ? true : false);

        if (SelectedBuilding == null)
            return highlighter;
        else
            return SelectedBuilding;
    }

    //To send object pool, clear data of building
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

    //Create a building and make it selected building
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
