using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    [Inject]
    PlacementManager placementManager;
    [SerializeField] private GameObject SelectedSoldier;

    [SerializeField] private GameObject EmptyAreaMovemenPrefab;
    [SerializeField] private LayerMask layerMask;


    void Update()
    {
        CheckClickOnUnits();
    }

    private void CheckClickOnUnits()
    {
        if (IsPointerOverUI()) return;

        //LEFT CLICK PROCESS
        if (Input.GetMouseButtonDown(0))
        {
            // Vector3 clickPosition = Input.mousePosition;
            // clickPosition.z = -Camera.main.transform.position.z; //DERINLIK AYARI
            Vector2 worldClickPosition = GetMouseClickPosition();

            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Soldier>())
                {
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

        //RIGHT CLICK PROCESS
        else if (Input.GetMouseButtonDown(1))
        {
            if (SelectedSoldier == null) return;

            Vector2 worldClickPosition = GetCurrentTilePosition();
            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                Soldier selectedSoldier = SelectedSoldier.GetComponent<Soldier>();

                if (hit.collider.GetComponent<Soldier>())
                {
                    Soldier targetSoldier = hit.collider.GetComponent<Soldier>();

                    if (selectedSoldier._teamTypes == targetSoldier._teamTypes) return;

                    EventManager.Broadcast(GameEvent.OnClickToAttack, SelectedSoldier, targetSoldier.gameObject);
                }
                else if (hit.collider.GetComponent<Building>())
                {
                    Building targetBuilding = hit.collider.GetComponent<Building>();

                    if (selectedSoldier._teamTypes == targetBuilding._teamTypes) return;

                    EventManager.Broadcast(GameEvent.OnClickToAttack, SelectedSoldier, targetBuilding.gameObject);
                }
                else
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
