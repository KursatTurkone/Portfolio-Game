using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLevel", menuName = "ScriptableObjects/EnemyLevel", order = 1)]
public class EnemyLevelSO : MonoBehaviour
{
    public int HowManyEnemiesWillSpawn;
    public int EnemySpawnRate;
    public List<EnemyTypes> EnemyTypes; 
}

[Serializable]
public class EnemyTypes
{
    public GameObject EnemyPrefab;
    public int EnemyDamage; 
    public int EnemyHealth;
    public int EnemySpeed;
}
