using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoldierProperties : ISoldier
{
    public abstract string Name { get; }
    public abstract Sprite SoldierSprite { get; }
    public abstract int Health { get; }
    public abstract int Damage { get; }
    public abstract int CellSize { get; }
    public abstract SoldierType SoldierType { get; }

    public abstract void SpawnForInformationMenu(SoldierType soldier, TeamTypes teamTypes, Transform ContentParent);
    public abstract void CreateSoldier(TeamTypes teamTypes, Transform spawnLocation);
}
