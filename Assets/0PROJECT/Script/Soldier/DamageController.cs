using ParticleFactoryStatic;
using UnityEngine;

/// <summary>
/// The class that controls the damage system of all damageable objects
/// </summary>

public class DamageController : MonoBehaviour, IHittable
{
    [SerializeField] private UnitType unitType;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int CurrentHealth;
    private SoldierAnim soldierAnim;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        soldierAnim = GetComponentInChildren<SoldierAnim>();
    }

    //Assign health values when object is created
    public void SetHealthBarValues(int health, Color teamColor)
    {
        CurrentHealth = health;
        healthBar.SetMaxHealth(CurrentHealth);
        healthBar.SetVisualProperties(teamColor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(Random.Range(1, 5));
        }
    }

    public void GiveDamage(GameObject targetUnit, int damageAmount)
    {
        if (targetUnit == null) return;

        DamageController targetDamageController = targetUnit.GetComponent<DamageController>();
        targetDamageController.TakeDamage(damageAmount);
    }

    public void TakeDamage(int damage)
    {
        //If a soldier takes damage, play animation
        if (unitType == UnitType.Soldier)
            soldierAnim.DamageTakenAnimation();

        SpawnHitParticle();

        CurrentHealth -= damage;
        if (CurrentHealth <= 0)  //Death
        {
            DeathProcess();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            return;
        }

        healthBar.SetHealth(CurrentHealth);
    }

    //Spawn particle according to the unit type
    void SpawnHitParticle()
    {
        switch (unitType)
        {
            case UnitType.Soldier:
                ParticleFactory.SpawnParticle(ParticleType.HitParticle, transform.position);
                break;

            case UnitType.Building:
                ParticleFactory.SpawnParticle(ParticleType.HitParticle, transform.GetChild(0).transform.position + Vector3.one * Random.Range(-0.32f, 0.32f));
                break;
        }
    }

    void DeathProcess()
    {
        switch (unitType)
        {
            case UnitType.Soldier:
                EventManager.Broadcast(GameEvent.OnPlaySound, "SoundDeath");
                break;

            case UnitType.Building:
                ParticleFactory.SpawnParticle(ParticleType.ExplodeParticle, transform.GetChild(0).transform.position);
                break;
        }

    }
}
