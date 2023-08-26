
using System.Collections.Generic;
using UnityEngine;

public interface ISoldier
{
    string Name { get; }
    Sprite SoldierSprite { get; }
    int Health { get; }
    int Damage { get; }
    int CellSize { get; }
    SoldierType SoldierType { get; }

}

public interface IBuilding
{
    string Name { get; }
    string Description { get; }
    Sprite BuildingSprite { get; }
    Sprite BuildingInformationSprite { get; }
    int Health { get; }
    int CellSize { get; }
    BuildingType BuildingType { get; }
    List<SoldierType> BuildingUnits { get; }
}

public interface IHittable
{
    void SetHealthBarValues(int health, Color teamColor);
    void TakeDamage(int damage);
}


