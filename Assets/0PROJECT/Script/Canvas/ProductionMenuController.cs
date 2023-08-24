using System;
using System.Collections;
using System.Collections.Generic;
using BuildingFactoryStatic;
using UnityEngine;
using Zenject;

public class ProductionMenuController : MonoBehaviour
{
    [Inject]
    ScrollContent scrollContent;

    [SerializeField] private Transform ContentParent;
    void Start()
    {
        CreateProductionElements();
    }


    private void CreateProductionElements()
    {
        int BuildingTypeCount = Enum.GetNames(typeof(BuildingType)).Length;
        int TeamTypeCount = Enum.GetNames(typeof(TeamTypes)).Length;

        for (int i = TeamTypeCount - 1; i >= 0; i--)
        {
            for (int j = BuildingTypeCount - 1; j >= 0 ; j--)
            {
                BuildingType buildingType = (BuildingType)j;
                TeamTypes teamType = (TeamTypes)i;

                BuildingFactory.SpawnForProductionMenu(buildingType, teamType, ContentParent);
            }
        }

        scrollContent.UpdateScrollView();
    }
}
