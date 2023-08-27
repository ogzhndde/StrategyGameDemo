using System;
using System.Collections;
using System.Collections.Generic;
using ArmyFactoryStatic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

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

    public GameObject CurrentBuilding;
    [SerializeField] LayerMask buildingLayerMask;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        ClearInformationPanel();
    }

    void Update()
    {
        CheckClickOnBuilding();
    }

    private void CheckClickOnBuilding()
    {
        if (IsPointerOverUI()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            clickPosition.z = -Camera.main.transform.position.z; // Uygun derinlik ayarı
            Vector2 worldClickPosition = Camera.main.ScreenToWorldPoint(clickPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, buildingLayerMask);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Building>())
                {
                    Building building = hit.collider.GetComponent<Building>();

                    building.ClickPlacedBuilding();
                }
                else
                {
                    ClearInformationPanel();
                }
            }
            else
            {
                ClearInformationPanel();
            }
        }
    }

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
