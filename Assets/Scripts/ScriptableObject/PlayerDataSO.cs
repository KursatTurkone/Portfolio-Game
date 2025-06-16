using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public float baseFireRate;
    public float speededFireRate;
    
}