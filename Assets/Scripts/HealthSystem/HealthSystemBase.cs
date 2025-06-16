using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public abstract class HealthSystemBase : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private HealthBar healthBarUI;
    public int currentHealth = 100;
    public List<EnemyDetector> EnemyDetectors;

    private void Awake()
    {
        healthBarUI = LeanPool.Spawn(healthBar, transform.position, Quaternion.identity,
            FindAnyObjectByType<WorldCanvas>().transform);
        healthBarUI.GetComponent<WorldCanvasPositionSetter>().target = transform;
        healthBarUI.SetHealth(currentHealth / 100f);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return; // No damage if already dead

        currentHealth -= damage;
        healthBarUI.SetHealth((float)currentHealth / 100f);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnEnemyEnter(EnemyDetector enemyDetector)
    {
        if (!EnemyDetectors.Contains(enemyDetector))
        {
            EnemyDetectors.Add(enemyDetector);
        }
    }
    public void OnEnemyExit(EnemyDetector enemyDetector)
    {
        if (EnemyDetectors.Contains(enemyDetector))
        {
            EnemyDetectors.Remove(enemyDetector);
        }
    }

    public abstract void Heal(int amount);


    public abstract void Die();

}