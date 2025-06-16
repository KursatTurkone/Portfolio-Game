using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class PlayerCrossbow : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Bullets bulletPrefab;

    private void Start()
    {
        PlayerManager.Instance.ShootBullet += OnShootBullet;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.ShootBullet -= OnShootBullet;
    }

    private void OnShootBullet()
    {
       var bullet = LeanPool.Spawn(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
       bullet.SetTarget(PlayerManager.Instance.closestEnemy.transform);
    }
}