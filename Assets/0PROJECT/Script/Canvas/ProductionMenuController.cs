using System;
using System.Collections;
using System.Collections.Generic;
using BuildingFactoryStatic;
using UnityEngine;

public class ProductionMenuController : MonoBehaviour
{
    [SerializeField] private Transform ContentParent;
    void Start()
    {
        CreateProductionElements();
    }


    private void CreateProductionElements()
    {
        int BuildingTypeCount = Enum.GetNames(typeof(BuildingType)).Length;
        int TeamTypeCount = Enum.GetNames(typeof(TeamTypes)).Length;

        for (int i = 0; i < TeamTypeCount; i++)
        {
            for (int j = 0; j < BuildingTypeCount; j++)
            {
                BuildingType buildingType = (BuildingType)j;
                TeamTypes teamType = (TeamTypes)i;

                BuildingFactory.SpawnForProductionMenu(buildingType, teamType, ContentParent);
            }
        }
    }
}
