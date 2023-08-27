using System;
using BuildingFactoryStatic;
using UnityEngine;
using Zenject;

/// <summary>
/// The main code that controls the production menu. It adds all desired buildings in the UI at the start of the game.
/// </summary>
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
        //// Get all types of teams and buildings specified in the enum. 
        int BuildingTypeCount = Enum.GetNames(typeof(BuildingType)).Length;
        int TeamTypeCount = Enum.GetNames(typeof(TeamTypes)).Length;

        // Spawn all posibility of buttons in the production menu.
        for (int i = TeamTypeCount - 1; i >= 0; i--)
        {
            for (int j = BuildingTypeCount - 1; j >= 0 ; j--)
            {
                BuildingType buildingType = (BuildingType)j;
                TeamTypes teamType = (TeamTypes)i;

                BuildingFactory.SpawnForProductionMenu(buildingType, teamType, ContentParent);
            }
        }

        //Reassign positions of buttons.
        scrollContent.UpdateScrollView();
    }
}
