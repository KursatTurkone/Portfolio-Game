using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
     [SerializeField] private EnemyBrain enemyBrain;
     private Collider myCollider;

     private void Start()
     {
          TryGetComponent(out myCollider); 
     }

     private void OnTriggerEnter(Collider other)
   {
      other.TryGetComponent<HealthSystemBase>(out var playerTargetable);
        if (playerTargetable)
        {
             enemyBrain.AddTarget(playerTargetable.transform);
             playerTargetable.OnEnemyEnter(this);
        }
   }
   private void OnTriggerExit(Collider other)
   {
      other.TryGetComponent<HealthSystemBase>(out var playerTargetable);
        if (playerTargetable)
        {
             enemyBrain.RemoveTarget(playerTargetable.transform);
             playerTargetable.OnEnemyExit(this);
        }
   }
   public void RemoveTarget(Transform target)
   {
      if (enemyBrain)
      {
         enemyBrain.RemoveTarget(target);
      }
   }
}
