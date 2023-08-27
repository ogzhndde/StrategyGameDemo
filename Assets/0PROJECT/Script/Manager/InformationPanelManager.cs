using System;
using System.Collections.Generic;
using ArmyFactoryStatic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;


/// <summary>
/// The class that controls the screen where the information of the objects clicked on in the game is displayed. 
/// It keeps the necessary variables in it and makes the assignments to the defined places when necessary. 
/// It also controls the spawning buttons of soldiers from the barracks.
/// </summary>


[Serializable]
public struct PanelVariables
{
    public Image IMA_Building;
    public Image IMA_Flag;
    public TextMeshProUGUI TMP_BuildingName;
    public TextMeshProUGUI TMP_Healt;
    public TextMeshProUGUI TMP_Description;
    public GameObject OBJ_UnitPanel;
    public Transform ContentParent;
    public List<GameObject> InfoPanelSubObjects;
}

public class InformationPanelManager : SingletonManager<InformationPanelManager>
{
    [Inject]
    GameManager gameManager;
    public PanelVariables panelVariables;

    [Space(15)]
    [Header("Definitions")]
    public GameObject CurrentBuilding;
    private Animator anim;
    [SerializeField] LayerMask buildingLayerMask;

    void Start()
    {
        anim = GetComponent<Animator>();
        ClearInformationPanel();
    }

    void Update()
    {
        CheckClickOnBuilding();
    }

    //Checks if a building has been clicked by send ray
    private void CheckClickOnBuilding()
    {
        if (IsPointerOverUI()) return;

        if (Input.GetMouseButtonDown(0))
        {
            //Check mouse position
            Vector3 clickPosition = Input.mousePosition;
            clickPosition.z = -Camera.main.transform.position.z;
            Vector2 worldClickPosition = Camera.main.ScreenToWorldPoint(clickPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, buildingLayerMask);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Building>()) //If hit any building
                {
                    Building building = hit.collider.GetComponent<Building>();

                    building.ClickPlacedBuilding();
                }
                else // Click on another object
                {
                    ClearInformationPanel();
                }
            }
            else //Click on empty area
            {
                ClearInformationPanel();
            }
        }
    }

    //Rearranges the variables in the information panel according to the selected building
    private void UpdateInformationPanel(ref Building selectedBuilding, BuildingType buildingType, TeamTypes teamType)
    {
        AnimationControl(true);
        SubObjectsActivationCheck(true);

        panelVariables.IMA_Building.sprite = selectedBuilding.BuildingInformationSprite;
        panelVariables.IMA_Flag.color = selectedBuilding.CLR_BuildingColor;
        panelVariables.TMP_BuildingName.text = selectedBuilding.Name;
        panelVariables.TMP_Healt.text = "HP = " + selectedBuilding.Health;
        panelVariables.TMP_Description.text = selectedBuilding.Description;
        panelVariables.OBJ_UnitPanel.SetActive(true);

        if (selectedBuilding.BuildingUnits == null)
            return;

        foreach (var unit in selectedBuilding.BuildingUnits)
        {
            ArmyFactory.SpawnForInformationMenu(unit, teamType, panelVariables.ContentParent);
        }
    }

    //Clear all panel variables
    public void ClearInformationPanel()
    {
        AnimationControl(false);
        CurrentBuilding = null;

        int childCount = panelVariables.ContentParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            ObjectPoolManager.ReturnObjectToPool(panelVariables.ContentParent.GetChild(i).gameObject);
        }

        SubObjectsActivationCheck(false);
    }

    //For optimization, it turns off unnecessary UI elements when the panel is off the screen.
    void SubObjectsActivationCheck(bool state)
    {
        foreach (var item in panelVariables.InfoPanelSubObjects)
        {
            item.SetActive(state);
        }
    }

    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    //Sets the animation setting according to the selected building
    private void AnimationControl(bool state)
    {
        anim.SetBool("_isPanelActive", state);
    }


    //##########################        EVENTS      ###################################
    void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnClickPlacedBuilding, OnClickPlacedBuilding);
    }

    void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnClickPlacedBuilding, OnClickPlacedBuilding);
    }

    //It gets the values of the clicked building via the event and updates the panel.
    private void OnClickPlacedBuilding(object _selectedBuilding, object _buildingType, object _teamType)
    {
        GameObject building = (GameObject)_selectedBuilding;
        Building selectedBuilding = building.GetComponent<Building>();
        BuildingType buildingType = (BuildingType)_buildingType;
        TeamTypes teamType = (TeamTypes)_teamType;

        ClearInformationPanel();
        CurrentBuilding = building;
        UpdateInformationPanel(ref selectedBuilding, buildingType, teamType);
    }

}
