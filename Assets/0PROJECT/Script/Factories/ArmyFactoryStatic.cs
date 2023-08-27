using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
using Zenject;

namespace ArmyFactoryStatic
{
    public static class ArmyFactory
    {
        private static Dictionary<SoldierType, Func<SoldierProperties>> soldierFactories = new Dictionary<SoldierType, Func<SoldierProperties>>
        {
            { SoldierType.Rookie, () => new CreateRookie() },
            { SoldierType.Officer, () => new CreateOfficer() },
            { SoldierType.General, () => new CreateGeneral() }
        };

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

