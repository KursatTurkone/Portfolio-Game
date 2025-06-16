using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform CastleTransform;
    public static GameManager Instance { get; private set; }
    public Action OnPlayerLost;
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
    
    public void PlayerLost()
    {
        Debug.Log("Player lost");
        OnPlayerLost?.Invoke();
    }
}