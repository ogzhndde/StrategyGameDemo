using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BuildingFactoryStatic
{
    public static class BuildingFactory
    {
        private static Dictionary<BuildingType, Func<BuildingProperties>> buildingFactories = new Dictionary<BuildingType, Func<BuildingProperties>>
        {
            {BuildingType.Barrack, () => new CreateBarrack()},
            {BuildingType.PowerPlant, () => new CreatePowerPlants()},
            {BuildingType.House, () => new CreateHouse()},
            {BuildingType.Fence, () => new CreateFence()}
        };

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

    public class CreateBarrack : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.BarrackData.Name;
        public override string Description => manager.SO.BarrackData.Description;
        public override Sprite BuildingSprite => manager.SO.BarrackData.BuildingSprite;
        public override int Health => manager.SO.BarrackData.Health;
        public override int CellSize => manager.SO.BarrackData.CellSize;
        public override BuildingType BuildingType => manager.SO.BarrackData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.BarrackData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;

            float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float mousePosZ = 0;
            Vector3 mousePos = new(mousePosX, mousePosY, mousePosZ);

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.BarrackData.UnitPrefab, mousePos, Quaternion.identity);
            var building = menuBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes, manager.SO.BarrackData.UnitPrefabs);
        }
    }


    public class CreatePowerPlants : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.PowerPlantData.Name;
        public override string Description => manager.SO.PowerPlantData.Description;
        public override Sprite BuildingSprite => manager.SO.PowerPlantData.BuildingSprite;
        public override int Health => manager.SO.PowerPlantData.Health;
        public override int CellSize => manager.SO.PowerPlantData.CellSize;
        public override BuildingType BuildingType => manager.SO.PowerPlantData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.PowerPlantData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;

            float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float mousePosZ = 0;
            Vector3 mousePos = new(mousePosX, mousePosY, mousePosZ);

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.PowerPlantData.UnitPrefab, mousePos, Quaternion.identity);
            var building = menuBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
        }
    }


    public class CreateHouse : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.HouseData.Name;
        public override string Description => manager.SO.HouseData.Description;
        public override Sprite BuildingSprite => manager.SO.HouseData.BuildingSprite;
        public override int Health => manager.SO.HouseData.Health;
        public override int CellSize => manager.SO.HouseData.CellSize;
        public override BuildingType BuildingType => manager.SO.HouseData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.HouseData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;

            float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float mousePosZ = 0;
            Vector3 mousePos = new(mousePosX, mousePosY, mousePosZ);

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.HouseData.UnitPrefab, mousePos, Quaternion.identity);
            var building = menuBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
        }
    }


    public class CreateFence : BuildingProperties
    {
        [Inject]
        GameManager manager;
        public override string Name => manager.SO.FenceData.Name;
        public override string Description => manager.SO.FenceData.Description;
        public override Sprite BuildingSprite => manager.SO.FenceData.BuildingSprite;
        public override int Health => manager.SO.FenceData.Health;
        public override int CellSize => manager.SO.FenceData.CellSize;
        public override BuildingType BuildingType => manager.SO.FenceData.BuildingType;

        public override void SpawnForProductionMenu(BuildingType buildingType, TeamTypes teamTypes, Transform ContentParent)
        {
            manager = GameManager.Instance;

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.FenceData.ProductionMenuPrefab, ContentParent);
            var building = menuBuilding.GetComponent<BuildingMenu>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
            building.SetVisualProperties();
        }

        public override void SpawnForPlacement(BuildingType buildingType, TeamTypes teamTypes)
        {
            manager = GameManager.Instance;

            float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float mousePosZ = 0;
            Vector3 mousePos = new(mousePosX, mousePosY, mousePosZ);

            var menuBuilding = ObjectPoolManager.SpawnObjects(manager.SO.FenceData.UnitPrefab, mousePos, Quaternion.identity);
            var building = menuBuilding.GetComponent<Building>();

            building.SetBuildingProperties(Name, Description, BuildingSprite, Health, CellSize, BuildingType, teamTypes);
        }
    }

}
