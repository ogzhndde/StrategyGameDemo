
using System.Collections.Generic;
using UnityEngine;

public interface ISoldier
{
    string Name { get; }
    int Health { get; }
    int Damage { get; }
    int CellSize { get; }
    SoldierType SoldierType { get; }

}

public interface IBuilding
{
    string Name { get; }
    string Description { get; }
    int Health { get; }
    int CellSize { get; }
    BuildingType BuildingType { get; }
    List<GameObject> BuildingUnits { get; }
}


