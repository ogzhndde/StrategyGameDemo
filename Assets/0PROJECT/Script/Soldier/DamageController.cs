using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour, IHittable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int CurrentHealth;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

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
        if(targetUnit == null) return;
        
        DamageController targetDamageController = targetUnit.GetComponent<DamageController>();
        targetDamageController.TakeDamage(damageAmount);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            return;
        }

        healthBar.SetHealth(CurrentHealth);
    }
}
