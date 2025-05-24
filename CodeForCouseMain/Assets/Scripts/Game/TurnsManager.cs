using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TurnsManager : MonoBehaviour
{
    public bool isPlayerTurn = true;
    int currentEnemyIndex = 0;

    List<Enemy> enemies;

    public UnityEvent<bool> OnTurnSideChange;

    public void PlayerTurn()
    {

    }
    
    public void EndPlayerTurn() 
    {
        isPlayerTurn = false;
        OnTurnSideChange.Invoke(isPlayerTurn);

        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
        EnemyTurn();
    }

    public void EnemyTurn()
    {
        currentEnemyIndex = 0;
        TryPerformEnemyActions();
    }

    private void TryPerformEnemyActions()
    {
        if (currentEnemyIndex <= enemies.Count)
        {
            enemies[currentEnemyIndex].OnMyTurnEnd.AddListener(OnEnemyTurnEnded);
            enemies[currentEnemyIndex].PerformTurn();
        }
        else
        {
            EndEnemiesTurn();
        }
    }

    public void OnEnemyTurnEnded()
    {
        enemies[currentEnemyIndex].OnMyTurnEnd.RemoveListener(OnEnemyTurnEnded);
        currentEnemyIndex++;
        TryPerformEnemyActions();
    }
    private void EndEnemiesTurn()
    {
        isPlayerTurn = true;
        OnTurnSideChange.Invoke(isPlayerTurn);
    }
}
