using Pathfinding;
using UnityEngine;

/// <summary>
/// A class that handles all animation controls of the character and holds animation events.
/// </summary>

public class SoldierAnim : MonoBehaviour
{
    [Header("Control Variables")]
    [SerializeField] private SoldierState soldierStateEnum;
    [SerializeField] private GameObject _currentTarget;
    private float _attackCooldown;
    private bool _isInCombat = false;

    [Space(15)]
    [Header("Definitions")]
    private Soldier soldier;
    private DamageController damageController;
    private Animator soldierAnim;
    private AIPath aIPath;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float defaultAttackCooldown;


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

    //Get current target of soldier and set to use
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

    //Set animation values according to soldier state.
    void SetAnimationValues()
    {
        _isInCombat = false;

        float stateValue = 0; //Blending tree value
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

    //Attack cooldown
    void ChargeTimer()
    {
        if (!_isInCombat) return;

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

    //Set the soldier facing direction
    void CheckSpriteDirection()
    {
        bool flipX = false;

        switch (_isInCombat)
        {
            case true: //If there is a target, face to target
                float targetWorldXPos = _currentTarget.transform.position.x;
                float soldiertWorldXPos = transform.position.x;

                flipX = targetWorldXPos > soldiertWorldXPos ? false : true;
                break;

            case false: //If there is no target, face to path
                flipX = aIPath.desiredVelocity.x > 0.01f ? false : true;
                break;
        }

        spriteRenderer.flipX = flipX;
    }
}
