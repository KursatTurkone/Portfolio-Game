using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : HealthSystemBase
{
    [SerializeField] private EnemyBrain enemyBrain;
    [SerializeField] private Coin CoinPrefab;
    [SerializeField] private int coinAmount = 5;

    private void OnEnable()
    {
        currentHealth = 100; 
    }

    public override void Heal(int amount)
    {
    }

    public override void Die()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            var coin = LeanPool.Spawn(CoinPrefab, transform.position, Quaternion.identity);
            coin.coinRigidbody.AddForce(Random.insideUnitSphere * 5, ForceMode.Impulse);
        }

        enemyBrain.EnemyDied();
    }
}