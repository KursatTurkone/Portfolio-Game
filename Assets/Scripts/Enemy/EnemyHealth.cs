using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystemBase
{
    [SerializeField] private EnemyBrain enemyBrain; 
    public override void Heal(int amount)
    {
     
    }

    public override void Die()
    {  
     enemyBrain.EnemyDied();
    }
}
