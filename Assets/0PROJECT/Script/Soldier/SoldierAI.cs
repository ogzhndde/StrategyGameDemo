using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Pathfinding;
using System.Linq;
using System;

public class SoldierAI : MonoBehaviour
{

    [Header("Definitions")]
    private Soldier soldier;
    private SoldierAnim soldierAnim;
    private AIPath aIPath;
    private Collider2D coll;


    [Space(10)]
    [Header("Variables")]
    public TeamTypes teamTypes;
    [SerializeField] private float SearchingRadius;
    [SerializeField] private Transform TargetDestination;
    [SerializeField] private List<GameObject> EnemiesTargetAround;
    [SerializeField] private Collider2D[] CollidersAround;
    [SerializeField] private LayerMask TargetLayers;
    [SerializeField] bool _isAlreadyFighting = false;


    void Awake()
    {
        soldier = GetComponent<Soldier>();
        soldierAnim = GetComponentInChildren<SoldierAnim>();
        aIPath = GetComponent<AIPath>();
        coll = GetComponent<Collider2D>();
    }


    void Update()
    {
        CheckEndReach();

    }

    private void FixedUpdate()
    {
        CheckAround();
        CheckCombatState();
    }

    public void SetNewDestination(Transform target)
    {
        TargetDestination = target;
        aIPath.destination = TargetDestination.position;
    }

    private void CheckAround()
    {
        if (TargetDestination == null) return;

        CollidersAround = Physics2D.OverlapCircleAll(transform.position, SearchingRadius, TargetLayers);
        EnemiesTargetAround.Clear();

        foreach (Collider2D collider in CollidersAround)
        {
            if (collider != coll)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    Building building = collider.GetComponent<Building>();
                    if (building._teamTypes != teamTypes)
                        EnemiesTargetAround.Add(collider.gameObject);
                }
                if (collider.gameObject.layer == LayerMask.NameToLayer("Soldier"))
                {
                    Soldier soldier = collider.GetComponent<Soldier>();
                    if (soldier._teamTypes != teamTypes)
                        EnemiesTargetAround.Add(collider.gameObject);
                }
            }
        }
    }

    private void CheckCombatState()
    {
        if (TargetDestination == null) return;

        if (EnemiesTargetAround.Contains(TargetDestination.gameObject))
        {
            _isAlreadyFighting = true;
            soldierAnim.SetSoldierState(SoldierState.Fight);
            soldierAnim.SetSoldierTarget(_isThereAnyTarget: true, TargetDestination.gameObject);
            aIPath.canMove = false;
        }
        else
        {
            _isAlreadyFighting = false;
            aIPath.canMove = true;
            soldierAnim.SetSoldierTarget(_isThereAnyTarget: false);

            if (aIPath.desiredVelocity.magnitude > 0.01)
            {
                soldierAnim.SetSoldierState(SoldierState.Run);
            }
            else
            {
                soldierAnim.SetSoldierState(SoldierState.Idle);
            }
        }
    }

    private void CheckEndReach()
    {
        if (TargetDestination == null) return;
        if (_isAlreadyFighting) return;

        if (Vector2.Distance(transform.position, TargetDestination.position) < aIPath.endReachedDistance)
        {
            ResetMovementValues();
        }
    }

    void ResetMovementValues()
    {
        TargetDestination = null;
        aIPath.canMove = false;
        soldierAnim.SetSoldierState(SoldierState.Idle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SearchingRadius);
    }


    //##########################        EVENTS      ###################################
    void OnEnable()
    {

        EventManager.AddHandler(GameEvent.OnClickToAttack, OnClickToAttack);
        EventManager.AddHandler(GameEvent.OnClickToMove, OnClickToMove);
    }

    void OnDisable()
    {
        ResetMovementValues();
        EventManager.RemoveHandler(GameEvent.OnClickToAttack, OnClickToAttack);
        EventManager.RemoveHandler(GameEvent.OnClickToMove, OnClickToMove);

    }



    private void OnClickToAttack(object selectedSoldier, object selectedTarget)
    {
        if ((GameObject)selectedSoldier != gameObject)
            return;

        GameObject targetUnit = (GameObject)selectedTarget;

        SetNewDestination(targetUnit.transform);
    }

    private void OnClickToMove(object selectedSoldier, object selectedLocation)
    {
        if ((GameObject)selectedSoldier != gameObject)
            return;

        GameObject targetLocation = (GameObject)selectedLocation;

        SetNewDestination(targetLocation.transform);
    }
}