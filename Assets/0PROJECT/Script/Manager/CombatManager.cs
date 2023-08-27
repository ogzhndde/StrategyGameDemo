using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

/// <summary>
/// Manager class where all audio playbacks in the game are controlled. 
/// It draws and plays the necessary sound sources with events. 
/// There are also various methods for fine tuning.
/// </summary>

public class CombatManager : MonoBehaviour
{
    [Inject]
    PlacementManager placementManager;

    [Header("Control Variables")]
    [SerializeField] private GameObject SelectedSoldier;
    [SerializeField] private LayerMask layerMask;


    [Space(15)]
    [Header("Definitions")]
    [SerializeField] private GameObject EmptyAreaMovemenPrefab;


    void Update()
    {
        CheckClickOnUnits();
    }

    private void CheckClickOnUnits()
    {
        //If it is on UI, return the method
        if (IsPointerOverUI()) return;

        //Left click operations
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldClickPosition = GetMouseClickPosition();

            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Soldier>())
                {
                    //If ray hit any soldier, select the soldier
                    SelectedSoldier = hit.collider.gameObject;
                    EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSelectSoldier");
                }
                else
                {
                    SelectedSoldier = null;
                }
            }
            else
            {
                SelectedSoldier = null;
            }
        }

        //Right Click Operations
        else if (Input.GetMouseButtonDown(1))
        {
            if (SelectedSoldier == null) return;

            Vector2 worldClickPosition = GetCurrentTilePosition();
            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, layerMask);

            //The ray we send can hit various objects. Buildings, another soldier or empty space. Operations are performed according to these situations.
            if (hit.collider != null)
            {
                Soldier selectedSoldier = SelectedSoldier.GetComponent<Soldier>();

                if (hit.collider.GetComponent<Soldier>()) //If hit any ENEMY soldier
                {
                    Soldier targetSoldier = hit.collider.GetComponent<Soldier>();

                    if (selectedSoldier._teamTypes == targetSoldier._teamTypes) return;
                    EventManager.Broadcast(GameEvent.OnClickToAttack, SelectedSoldier, targetSoldier.gameObject);
                }

                else if (hit.collider.GetComponent<Building>())//If hit any ENEMY building
                {
                    Building targetBuilding = hit.collider.GetComponent<Building>();

                    if (selectedSoldier._teamTypes == targetBuilding._teamTypes) return;
                    EventManager.Broadcast(GameEvent.OnClickToAttack, SelectedSoldier, targetBuilding.gameObject);
                }

                else // Click on an empty space
                {
                    JustMove();
                }
            }
            else
            {
                JustMove();
            }
        }
    }

    // Get the click tile location and make selected soldier move there
    void JustMove()
    {
        EmptyAreaMovemenPrefab.transform.position = GetCurrentTilePosition();
        EventManager.Broadcast(GameEvent.OnClickToMove, SelectedSoldier, EmptyAreaMovemenPrefab);
        EventManager.Broadcast(GameEvent.OnPlaySound, "SoundJustMove");
    }

    private Vector2 GetMouseClickPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return new Vector2(worldPosition.x, worldPosition.y);
    }

    private Vector3 GetCurrentTilePosition()
    {
        return placementManager.cellWorldPos;
    }


    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
