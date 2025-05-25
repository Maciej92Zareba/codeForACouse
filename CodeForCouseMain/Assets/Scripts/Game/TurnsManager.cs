using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] EnemiesManager enemiesManager;
    [SerializeField] DeckManager deckManager;
    [SerializeField] EconomyManager economyManager;

    [HideInInspector] public bool isPlayerTurn = true;
    [HideInInspector] public int roundsPlayed = 0;
    private int currentEnemyIndex = 0;

    [SerializeField] int currecnyToAddAfterTurnEnd = 10;

    private void Start()
    {
        deckManager.GiveStartingCards();
        PlayerTurn();
        StartCoroutine(DelayedCall());
    }

    private IEnumerator DelayedCall ()
    {
        yield return null;
        GlobalActions.Instance.NotifyOnOnTurnChange(isPlayerTurn);
    }

    public void PlayerTurn()
    {
        deckManager.CardSelectionEvent();
    }

    [Button]
    public void EndPlayerTurn() 
    {
        isPlayerTurn = false;
        GlobalActions.Instance.NotifyOnOnTurnChange(isPlayerTurn);
        economyManager.AddCurrency(currecnyToAddAfterTurnEnd);
        EnemyTurn();
    }

    [Tooltip("Simulate 1 Enemy Turn")] [Button]
    public void EnemyTurn()
    {
        currentEnemyIndex = 0;
        TryPerformEnemyActions();
    }

    private void TryPerformEnemyActions()
    {
        if (currentEnemyIndex <= enemiesManager.enemies.Count)
        {
            //enemiesManager.enemies[currentEnemyIndex].OnMyTurnEnd.AddListener(OnEnemyTurnEnded);
            //enemiesManager.enemies[currentEnemyIndex].PerformTurn();
        }
        else
        {
            EndEnemiesTurn();
        }
    }

    public void OnEnemyTurnEnded()
    {
        enemiesManager.enemies[currentEnemyIndex].OnMyTurnEnd.RemoveListener(OnEnemyTurnEnded);
        currentEnemyIndex++;
        TryPerformEnemyActions();
    }

    [Button]
    private void EndEnemiesTurn()
    {
        isPlayerTurn = true;
        GlobalActions.Instance.NotifyOnOnTurnChange(isPlayerTurn);
        roundsPlayed++;
        PlayerTurn();
    }
}
