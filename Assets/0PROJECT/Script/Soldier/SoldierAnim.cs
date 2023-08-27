using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SoldierAnim : MonoBehaviour
{
    [SerializeField] private SoldierState soldierStateEnum;
    [SerializeField] private GameObject _currentTarget;
    private Soldier soldier;
    private DamageController damageController;
    private Animator soldierAnim;
    private AIPath aIPath;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float defaultAttackCooldown;
    private float _attackCooldown;
    private bool _isInCombat = false;


    void Awake()
    {
        soldierAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soldier = GetComponentInParent<Soldier>();
        damageController = GetComponentInParent<DamageController>();
        aIPath = GetComponentInParent<AIPath>();

        InvokeRepeating(nameof(CheckSpriteDirection), 0, 0.2f);
    }

    void Update()
    {
        SetAnimationValues();
    }

    public void SetSoldierState(SoldierState soldierState)
    {
        soldierStateEnum = soldierState;
    }

    public void SetSoldierTarget(bool _isThereAnyTarget, GameObject currentTarget = null)
    {
        if (_isThereAnyTarget == true)
            _currentTarget = currentTarget;
        else
            _currentTarget = null;
    }

    public void Attack()
    {
        damageController.GiveDamage(_currentTarget, soldier.Damage);
    }

    void SetAnimationValues()
    {
        _isInCombat = false;

        float stateValue = 0;
        switch (soldierStateEnum)
        {
            case SoldierState.Idle:
                stateValue = 0;
                _attackCooldown = defaultAttackCooldown;
                break;
            case SoldierState.Run:
                stateValue = 1;
                _attackCooldown = defaultAttackCooldown;
                break;
            case SoldierState.Fight:
                stateValue = 0;
                _isInCombat = true;
                ChargeTimer();
                break;

        }

        soldierAnim.SetFloat("stateValue", stateValue);
    }

    void ChargeTimer()
    {
        if (!_isInCombat)
            return;

        _attackCooldown -= Time.deltaTime;

        if (_attackCooldown <= 0)
        {
            _attackCooldown = defaultAttackCooldown;
            soldierAnim.SetTrigger("_attack");
        }
    }

    public void DamageTakenAnimation()
    {
        soldierAnim.SetTrigger("_damageTaken");
        
        _attackCooldown = defaultAttackCooldown;
    }

    void CheckSpriteDirection()
    {
        bool flipX = false;

        switch (_isInCombat)
        {
            case true:
                float targetWorldXPos = _currentTarget.transform.position.x;
                float soldiertWorldXPos = transform.position.x;

                flipX = targetWorldXPos > soldiertWorldXPos ? false : true;
                break;

            case false:
                flipX = aIPath.desiredVelocity.x > 0.01f ? false : true;
                break;
        }

        spriteRenderer.flipX = flipX;
    }
}
