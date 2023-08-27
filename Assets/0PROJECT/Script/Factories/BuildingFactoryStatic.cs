using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// The class that produces all buildings that can be produced.
/// There is a main production class called BuildingFactory, and it provides the production of buildings where necessary by pulling the specific data of the building types.
/// There are 4 types of buildings: Barracks, Power Plants, Houses and Fences.
/// If a new type of building is to be added, it is sufficient to add it here.
/// </summary>

namespace BuildingFactoryStatic
{
    //It stores all building types and production classes in a dictionary.
    public static class BuildingFactory
    {
        private static Dictionary<BuildingType, Func<BuildingProperties>> buildingFactories = new Dictionary<BuildingType, Func<BuildingProperties>>
        {
            {BuildingType.Barrack, () => new CreateBarrack()},
            {BuildingType.PowerPlant, () => new CreatePowerPlants()},
            {BuildingType.House, () => new CreateHouse()},
            {BuildingType.Fence, () => new CreateFence()}
        };

        //If a UI Button is to be spawned for the production panel, it is produced here.
        public static BuildingProperties SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            if (buildingFactories.TryGetValue(buildingType, out var factory))
            {
                var building = factory.Invoke();
                building.SpawnForProductionMenu(buildingType, teamTypes, ContentParent);
                return building;
            }
            else
                return null;
        }

        //The class in which building are created.
        public static BuildingProperties SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            if (buildingFactories.TryGetValue(buildingType, out var factory))
            {
                var building = factory.Invoke();
                building.SpawnForPlacement(buildingType, teamTypes);
                return building;
            }
            else
                return null;
        }
    }

    //All necessary data for buildings is drawn from scriptable objects and sent to the factory for production.
    public class CreateBarrack : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.BarrackData.Name;
        public override string Description => manager.SO.BarrackData.Description;
        public override Sprite BuildingSprite => manager.SO.BarrackData.BuildingSprite;
        public override Sprite BuildingInformationSprite => manager.SO.BarrackData.BuildingInformationSprite;
        public override int Health => manager.SO.BarrackData.Health;
        public override int CellSize => manager.SO.BarrackData.CellSize;
        public override BuildingType BuildingType => manager.SO.BarrackData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.BarrackData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;
            Vector3 mousePos = GetMousePosition();

            var spawnedBuilding = ObjectPoolManager.SpawnObjects(manager.SO.BarrackData.BuildingPrefab, mousePos, Quaternion.identity);
            var building = spawnedBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes, manager.SO.BarrackData.UnitTypes);
            building.SetVisualProperties();
            building.CreateBuilding();
        }
    }

    //All necessary data for buildings is drawn from scriptable objects and sent to the factory for production.
    public class CreatePowerPlants : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.PowerPlantData.Name;
        public override string Description => manager.SO.PowerPlantData.Description;
        public override Sprite BuildingSprite => manager.SO.PowerPlantData.BuildingSprite;
        public override Sprite BuildingInformationSprite => manager.SO.PowerPlantData.BuildingInformationSprite;
        public override int Health => manager.SO.PowerPlantData.Health;
        public override int CellSize => manager.SO.PowerPlantData.CellSize;
        public override BuildingType BuildingType => manager.SO.PowerPlantData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.PowerPlantData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;
            Vector3 mousePos = GetMousePosition();

            var spawnedBuilding = ObjectPoolManager.SpawnObjects(manager.SO.PowerPlantData.BuildingPrefab, mousePos, Quaternion.identity);
            var building = spawnedBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
            building.CreateBuilding();
        }
    }

    //All necessary data for buildings is drawn from scriptable objects and sent to the factory for production.
    public class CreateHouse : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.HouseData.Name;
        public override string Description => manager.SO.HouseData.Description;
        public override Sprite BuildingSprite => manager.SO.HouseData.BuildingSprite;
        public override Sprite BuildingInformationSprite => manager.SO.HouseData.BuildingInformationSprite;
        public override int Health => manager.SO.HouseData.Health;
        public override int CellSize => manager.SO.HouseData.CellSize;
        public override BuildingType BuildingType => manager.SO.HouseData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.HouseData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;
            Vector3 mousePos = GetMousePosition();

            var spawnedBuilding = ObjectPoolManager.SpawnObjects(manager.SO.HouseData.BuildingPrefab, mousePos, Quaternion.identity);
            var building = spawnedBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
            building.CreateBuilding();
        }
    }

    //All necessary data for buildings is drawn from scriptable objects and sent to the factory for production.
    public class CreateFence : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.FenceData.Name;
        public override string Description => manager.SO.FenceData.Description;
        public override Sprite BuildingSprite => manager.SO.FenceData.BuildingSprite;
        public override Sprite BuildingInformationSprite => manager.SO.FenceData.BuildingInformationSprite;
        public override int Health => manager.SO.FenceData.Health;
        public override int CellSize => manager.SO.FenceData.CellSize;
        public override BuildingType BuildingType => manager.SO.FenceData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.FenceData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;
            Vector3 mousePos = GetMousePosition();

            var spawnedBuilding = ObjectPoolManager.SpawnObjects(manager.SO.FenceData.BuildingPrefab, mousePos, Quaternion.identity);
            var building = spawnedBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, BuildingInformationSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
            building.CreateBuilding();
        }
    }

}
