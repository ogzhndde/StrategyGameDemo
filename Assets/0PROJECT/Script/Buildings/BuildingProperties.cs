using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingProperties : IBuilding
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract Sprite BuildingSprite { get; }
    public abstract int Health { get; }
    public abstract int CellSize { get; }
    public abstract BuildingType BuildingType { get; }
    public List<GameObject> BuildingUnits { get; }

    public abstract void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent);
    public abstract void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes);

}
