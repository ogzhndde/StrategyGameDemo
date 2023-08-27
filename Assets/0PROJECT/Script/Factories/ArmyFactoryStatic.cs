using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// The class that produces all military units that can be produced.
/// There is a main production class called ArmyFactory, and it provides the production of soldiers where necessary by pulling the specific data of the soldier types.
/// There are 3 types of soldiers: Rookie, Officer and General.
/// If a new type of soldier is to be added, it is sufficient to add it here.
/// </summary>
namespace ArmyFactoryStatic
{
    public static class ArmyFactory
    {
        //It stores all soldier types and production classes in a dictionary.
        private static Dictionary<SoldierType, Func<SoldierProperties>> soldierFactories = new Dictionary<SoldierType, Func<SoldierProperties>>
        {
            { SoldierType.Rookie, () => new CreateRookie() },
            { SoldierType.Officer, () => new CreateOfficer() },
            { SoldierType.General, () => new CreateGeneral() }
        };

        //If a UI Button is to be spawned for the information panel, it is produced here.
        public static SoldierProperties SpawnForInformationMenu(SoldierType soldierType, TeamTypes teamTypes, Transform ContentParent)
        {
            if (soldierFactories.TryGetValue(soldierType, out var factory))
            {
                var soldier = factory.Invoke();
                soldier.SpawnForInformationMenu(soldierType, teamTypes, ContentParent);
                return soldier;
            }
            else
                return null;
        }

        //The class in which military units are created.
        public static SoldierProperties CreateSoldier(SoldierType soldierType, TeamTypes teamTypes, Transform SpawnLocation)
        {
            if (soldierFactories.TryGetValue(soldierType, out var factory))
            {
                var soldier = factory.Invoke();
                soldier.CreateSoldier(teamTypes, SpawnLocation);
                return soldier;
            }
            else
            {
                return null;
            }
        }
    }

    //All necessary data for soldiers is drawn from scriptable objects and sent to the factory for production.
    public class CreateRookie : SoldierProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.rookieData.Name;
        public override Sprite SoldierSprite => manager.SO.rookieData.soldierSprite;
        public override int Health => manager.SO.rookieData.health;
        public override int Damage => manager.SO.rookieData.damage;
        public override int CellSize => manager.SO.rookieData.cellSize; //Means 1x1
        public override SoldierType SoldierType => manager.SO.rookieData.soldierType;

        public override void SpawnForInformationMenu(SoldierType soldier, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuSoldier = ObjectPoolManager.SpawnObjects(manager.SO.rookieData.soldierUIPrefab, ContentParent);
            var soldierUI = menuSoldier.GetComponent<SoldierMenu>();

            soldierUI.SetSoldierProperties(Name, SoldierSprite, Health, Damage, CellSize, SoldierType, teamTypes);
            soldierUI.SetVisualProperties();
        }

        public override void CreateSoldier(TeamTypes teamTypes, Transform spawnLocation)
        {
            manager = GameManager.Instance;

            var spawnedSoldier = ObjectPoolManager.SpawnObjects(manager.SO.rookieData.soldierPrefab, spawnLocation.position, Quaternion.identity);
            var soldier = spawnedSoldier.GetComponent<Soldier>();

            soldier.SetSoldierProperties(Name, SoldierSprite, Health, Damage, CellSize, SoldierType, teamTypes);
            EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSpawnRookie");
        }
    }
    
    //All necessary data for soldiers is drawn from scriptable objects and sent to the factory for production. 
    public class CreateOfficer : SoldierProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.officerData.Name;
        public override Sprite SoldierSprite => manager.SO.officerData.soldierSprite;
        public override int Health => manager.SO.officerData.health;
        public override int Damage => manager.SO.officerData.damage;
        public override int CellSize => manager.SO.officerData.cellSize; //Means 1x1
        public override SoldierType SoldierType => manager.SO.officerData.soldierType;


        public override void SpawnForInformationMenu(SoldierType soldier, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuSoldier = ObjectPoolManager.SpawnObjects(manager.SO.rookieData.soldierUIPrefab, ContentParent);
            var soldierUI = menuSoldier.GetComponent<SoldierMenu>();

            soldierUI.SetSoldierProperties(Name, SoldierSprite, Health, Damage, CellSize, SoldierType, teamTypes);
            soldierUI.SetVisualProperties();
        }

        public override void CreateSoldier(TeamTypes teamTypes, Transform spawnLocation)
        {
            manager = GameManager.Instance;

            var spawnedSoldier = ObjectPoolManager.SpawnObjects(manager.SO.officerData.soldierPrefab, spawnLocation.position, Quaternion.identity);
            var soldier = spawnedSoldier.GetComponent<Soldier>();

            soldier.SetSoldierProperties(Name, SoldierSprite, Health, Damage, CellSize, SoldierType, teamTypes);
            EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSpawnOfficer");
        }
    }

    //All necessary data for soldiers is drawn from scriptable objects and sent to the factory for production.
    public class CreateGeneral : SoldierProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.GeneralData.Name;
        public override Sprite SoldierSprite => manager.SO.GeneralData.soldierSprite;
        public override int Health => manager.SO.GeneralData.health;
        public override int Damage => manager.SO.GeneralData.damage;
        public override int CellSize => manager.SO.GeneralData.cellSize; //Means 1x1
        public override SoldierType SoldierType => manager.SO.GeneralData.soldierType;

        public override void SpawnForInformationMenu(SoldierType soldier, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuSoldier = ObjectPoolManager.SpawnObjects(manager.SO.rookieData.soldierUIPrefab, ContentParent);
            var soldierUI = menuSoldier.GetComponent<SoldierMenu>();

            soldierUI.SetSoldierProperties(Name, SoldierSprite, Health, Damage, CellSize, SoldierType, teamTypes);
            soldierUI.SetVisualProperties();
        }

        public override void CreateSoldier(TeamTypes teamTypes, Transform spawnLocation)
        {
            manager = GameManager.Instance;

            var spawnedSoldier = ObjectPoolManager.SpawnObjects(manager.SO.GeneralData.soldierPrefab, spawnLocation.position, Quaternion.identity);
            var soldier = spawnedSoldier.GetComponent<Soldier>();

            soldier.SetSoldierProperties(Name, SoldierSprite, Health, Damage, CellSize, SoldierType, teamTypes);
            EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSpawnGeneral");
        }
    }
}

