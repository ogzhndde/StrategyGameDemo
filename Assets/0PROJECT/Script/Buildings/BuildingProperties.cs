using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of the all buildings.
/// </summary>

public abstract class BuildingProperties : IBuilding
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract Sprite BuildingSprite { get; }
    public abstract Sprite BuildingInformationSprite { get; }
    public abstract int Health { get; }
    public abstract int CellSize { get; }
    public abstract BuildingType BuildingType { get; }
    public List<SoldierType> BuildingUnits { get; }


    public abstract void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent);
    public abstract void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes);

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos;

        float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        float mousePosZ = 0;
        mousePos = new(mousePosX, mousePosY, mousePosZ);

        return mousePos;
    }

}
