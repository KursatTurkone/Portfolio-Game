using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int howManyEnemiesToSpawn = 5;
    [SerializeField] private float spawnDelay = 3f;
    private int currentEnemiesSpawned = 0;
    private void Start()
    {
        GameManager.Instance.AddEnemies(howManyEnemiesToSpawn);
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        LeanPool.Spawn(enemyPrefab, transform.position, transform.rotation);
        DOVirtual.DelayedCall(spawnDelay, () =>
        {
            if(howManyEnemiesToSpawn<=currentEnemiesSpawned)
                return;
            if (!GameManager.Instance.IsGameActive)
                return;
            currentEnemiesSpawned++;
            SpawnEnemies();
        });
    }
}