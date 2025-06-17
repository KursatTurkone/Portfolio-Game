using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 10;
    [SerializeField] private GameObject ExplosionPrefab;
    private bool startMoveForward = false;

    private void Start()
    {
        DOVirtual.DelayedCall(3f, () =>
        {
            if (!gameObject.activeSelf) return;
            LeanPool.Despawn(gameObject);
        });
    }

    private void Update()
    {
        if (!startMoveForward)
            return;
        transform.Translate(Vector3.forward * Time.deltaTime * 20f);
    }


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out EnemyHealth enemy);
        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
            LeanPool.Spawn(ExplosionPrefab, transform.position, Quaternion.identity);
            LeanPool.Despawn(this);
        }

        // Destroy the bullet after hitting anything and make an explosion effect
    }

    public void SetTarget(Transform target)
    {
        var targettedPoint = target.position;
        targettedPoint.y = 10;
        transform.DOMove(targettedPoint,   50f).SetSpeedBased().OnComplete(()=>startMoveForward = true).SetEase(Ease.Linear); 
    }
}