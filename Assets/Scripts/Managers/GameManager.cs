using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform CastleTransform;
    public bool IsGameActive=true; 
    public static GameManager Instance { get; private set; }
    public Action OnPlayerLost;
    public Action OnPlayerWon;
    public Action<int> OnCoinCountChanged;
    private int currentCoinCount;
    private int mostEnemiesAmount = 0;
    private int currentKillCount = 0;
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
        OnPlayerLost?.Invoke();
        IsGameActive = false;
    }

    public void PlayerWon()
    {
        OnPlayerWon?.Invoke();
        IsGameActive = false;
    }
    public void AddCoin()
    {
        currentCoinCount++;
        OnCoinCountChanged?.Invoke(currentCoinCount);
    }

    public void AddEnemies(int amount)
    {
        mostEnemiesAmount += amount;
    }

    public void IncreaseKillCount()
    {
        currentKillCount++; 
        if(currentKillCount>= mostEnemiesAmount)
            PlayerWon();
    }
}