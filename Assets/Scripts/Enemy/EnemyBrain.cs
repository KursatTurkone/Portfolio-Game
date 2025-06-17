using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyBrain : MonoBehaviour
{
    [FormerlySerializedAs("enemyDetector")] [SerializeField]
    private EnemyCollider enemyCollider;

    [SerializeField] private EnemyAnimationController enemyAnimationController;
    [SerializeField] private Transform TestTarget;
    [SerializeField] private int DamageAmount = 20;
    [SerializeField] private RagdollOperations ragdollOperations;
    NavMeshAgent navMeshAgent;
    private List<Transform> targets = new List<Transform>();
    private Transform closestTarget;
    private Transform castleTransform;
    private Transform currentSettedTarget;
    private bool cantMove = false;

    private void Start()
    {
        TryGetComponent<NavMeshAgent>(out navMeshAgent);
        castleTransform = GameManager.Instance.CastleTransform;
        SetTarget(castleTransform);
        GameManager.Instance.OnPlayerLost += PlayerLost;
    }

    private void OnEnable()
    {
        cantMove = false; 
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerLost -= PlayerLost;
    }

    private void PlayerLost()
    {
        if (cantMove)
            return;
        DisableEnemy();
        enemyAnimationController.PlayEnemyWinAnimation();
    }

    public void EnemyDied()
    {
        if (cantMove)
            return;
        DisableEnemy();
        ragdollOperations.DoRagdoll(true);
        DOVirtual.DelayedCall(3, () => ragdollOperations.ChangeLayerToDead()).OnComplete(()=>DOVirtual.DelayedCall(1f, () =>
        {
            PlayerManager.Instance.RemoveEnemy(enemyCollider);
            GameManager.Instance.IncreaseKillCount();
            LeanPool.Despawn(gameObject);
            ragdollOperations.ResetCharacter();
            cantMove = false; 
            targets.Clear();
            navMeshAgent.speed = 3.5f;
            enemyCollider.gameObject.SetActive(true);
            enemyAnimationController.PlayIdleAnimation();
            currentSettedTarget = null; 
        }));
      
    }

    private void DisableEnemy()
    {
        cantMove = true;
        navMeshAgent.speed = 0;
        enemyCollider.gameObject.SetActive(false);
        PlayerManager.Instance.RemoveEnemy(enemyCollider);
    }

    private void SetTarget(Transform target)
    {
        currentSettedTarget = target;
        navMeshAgent.destination = currentSettedTarget.position;
        enemyAnimationController.PlayWalkAnimation();
    }

    private void Update()
    {
        if (cantMove)
            return;
        //Find the closest target if there are multiple targets
        if (targets.Count > 0)
        {
            closestTarget = targets[0];
            float closestDistance = Vector3.Distance(transform.position, closestTarget.position);

            foreach (var target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                }
            }

            //If a target close enough, attack it
            if (CheckClosenest())
            {
                enemyAnimationController.PlayAttackAnimation();
                navMeshAgent.destination = transform.position;
            }

            if (closestTarget == currentSettedTarget)
                return;

            SetTarget(closestTarget);
        }
        else
        {
            if (castleTransform == currentSettedTarget)
                return;
            SetTarget(castleTransform);
        }
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
    }

    public void GiveDamage()
    {
        if (CheckClosenest())
        {
            closestTarget.TryGetComponent(out HealthSystemBase playerTargetable);
            if (playerTargetable)
                playerTargetable.TakeDamage(DamageAmount);
        }
    }

    public bool CheckClosenest()
    {
        closestTarget.TryGetComponent<Collider>(out var collider);
        Vector3 estimatedContactPoint = collider.ClosestPoint(transform.position);
        if (Vector3.Distance(transform.position, estimatedContactPoint) < 1f)
            return true;

        return false;
    }
}