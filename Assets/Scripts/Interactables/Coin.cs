using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Rigidbody coinRigidbody;

    private Transform target;

    private void Start()
    {
        target = PlayerManager.Instance.transform;
    }

    private void OnEnable()
    {
        DOVirtual.DelayedCall(2f, PullCoinsTowardsTarget);
    }

    public void PullCoinsTowardsTarget()
    {
        transform.DOJump(target.position, .5f, 1, 0.5f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                LeanPool.Despawn(this);
                GameManager.Instance.AddCoin();
            });
    }
}