using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCommonBuildings : HealthSystemBase
{
    [SerializeField] private GameObject ExplosionPrefab;
    public override void Heal(int amount)
    {
    }

    public override void Die()
    {
        LeanPool.Spawn(ExplosionPrefab, transform.position, Quaternion.identity);

        for (int i = 0; i < EnemyDetectors.Count; i++)
        {
            EnemyDetectors[i].RemoveTarget(transform);
        }
    }
}