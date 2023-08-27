using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// The class that controls the AIs of the soldiers. 
/// All movements, attack situations, target locations etc. is kept here.
/// </summary>

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

    //Set a new target to AI and make him go the target
    public void SetNewDestination(Transform target)
    {
        TargetDestination = target;
        aIPath.destination = TargetDestination.position;
    }

    //If there is a target, it scans and lists all targets in its vicinity.
    private void CheckAround()
    {
        if (TargetDestination == null) return;

        CollidersAround = Physics2D.OverlapCircleAll(transform.position, SearchingRadius, TargetLayers);
        EnemiesTargetAround.Clear();

        foreach (Collider2D collider in CollidersAround)
        {
            if (collider != coll) //Ignore own collider
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

    // If you reach the target, go into attack state.
    private void CheckCombatState()
    {
        if (TargetDestination == null) return;

        if (EnemiesTargetAround.Contains(TargetDestination.gameObject)) //If your target is around
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

            if (aIPath.desiredVelocity.magnitude > 0.01) //Check pathfinding magnitude to make decision of running or idleing
                soldierAnim.SetSoldierState(SoldierState.Run);
            else
                soldierAnim.SetSoldierState(SoldierState.Idle);
        }
    }

    //Check reach the target or not
    private void CheckEndReach()
    {
        if (TargetDestination == null) return;
        if (_isAlreadyFighting) return;

        if (Vector2.Distance(transform.position, TargetDestination.position) < aIPath.endReachedDistance)
        {
            ResetMovementValues();
        }
    }

    //For object pooling, reset values when death
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

    //Event for attacking a target
    private void OnClickToAttack(object selectedSoldier, object selectedTarget)
    {
        if ((GameObject)selectedSoldier != gameObject)
            return;

        GameObject targetUnit = (GameObject)selectedTarget;

        SetNewDestination(targetUnit.transform);
        EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSoldierCharge" + Random.Range(1, 3)); 
    }

    //Event for just move
    private void OnClickToMove(object selectedSoldier, object selectedLocation)
    {
        if ((GameObject)selectedSoldier != gameObject)
            return;

        GameObject targetLocation = (GameObject)selectedLocation;

        SetNewDestination(targetLocation.transform);
    }
}