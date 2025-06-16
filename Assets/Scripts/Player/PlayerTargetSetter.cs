using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetSetter : MonoBehaviour
{
 
   private void OnTriggerEnter(Collider other)
   {
      other.TryGetComponent<EnemyCollider>(out var enemyDetector); 
      if (enemyDetector)
      {
        PlayerManager.Instance.AddEnemy(enemyDetector);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      other.TryGetComponent<EnemyCollider>(out var enemyDetector); 
      if (enemyDetector)
      {
         PlayerManager.Instance.RemoveEnemy(enemyDetector);
      }
   }



}
