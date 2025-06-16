using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
   
   private void OnDisable()
   {
      PlayerManager.Instance.RemoveEnemy(this);
   }

   public void TakeDamage(int damage)
   {
      
   }
}
