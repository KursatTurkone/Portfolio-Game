using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerDataSo;
    private List<EnemyCollider> _enemies = new List<EnemyCollider>();
    [HideInInspector] public PlayerWalkControl _playerWalkControl;
    [HideInInspector] public PlayerAnimationController _playerAnimationController;
    private float currentFireRate;
    private int bulletCount;
    private Tween ShootTween;
    private bool isShooting;
    [HideInInspector] public EnemyCollider closestEnemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action ShootBullet;

    public static PlayerManager Instance { get; private set; }

    private void FixedUpdate()
    {
        if (_enemies.Count == 0)
        {
            _playerWalkControl.SetPlayerTarget(null);
            _playerAnimationController.StopShootMovement();
            bulletCount = 0;
            ShootTween.Kill();
            isShooting = false;
            return;
        }


        closestEnemy = null;
        float closestDistanceSqr = float.MaxValue;
        Vector3 currentPosition = transform.position;

        for (int i = 0; i < _enemies.Count; i++)
        {
            var enemy = _enemies[i];
            if (enemy == null)
                continue;

            Vector3 dirToEnemy = enemy.transform.position - currentPosition;
            float dSqrToEnemy = dirToEnemy.sqrMagnitude;

            if (dSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            _playerWalkControl.SetPlayerTarget(closestEnemy.transform);
            if (isShooting)
                return;
            isShooting = true;
            BulletShooting();
        }
    }

    private void BulletShooting()
    {
        currentFireRate = bulletCount < 2 ? playerDataSo.baseFireRate : playerDataSo.speededFireRate;
        _playerAnimationController.ShootAnimation();
        bulletCount++;
        ShootTween = DOVirtual.DelayedCall(currentFireRate, BulletShooting);
    }

    public void AddEnemy(EnemyCollider enemyCollider)
    {
        if (!_enemies.Contains(enemyCollider))
        {
            _enemies.Add(enemyCollider);
        }
    }

    public void RemoveEnemy(EnemyCollider enemyCollider)
    {
        if (_enemies.Contains(enemyCollider))
        {
            _enemies.Remove(enemyCollider);
        }
    }

    public void ShootBulletInvoke()
    {
        if (_enemies.Count == 0)
            return;
        ShootBullet?.Invoke();
    }
}