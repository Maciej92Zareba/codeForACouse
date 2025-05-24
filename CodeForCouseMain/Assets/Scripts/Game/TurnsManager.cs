using UnityEngine;
using UnityEngine.Events;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] EnemiesManager enemiesManager;
    [SerializeField] DeckManager deckManager;

    public bool isPlayerTurn = true;
    int currentEnemyIndex = 0;
    public int roundsPlayed = 0;

    public UnityEvent<bool> OnTurnSideChange;

    private void Start()
    {
        deckManager.GiveStartingCards();
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        deckManager.CardSelectionEvent();
        deckManager.DrawCard();
    }
    
    public void EndPlayerTurn() 
    {
        isPlayerTurn = false;
        OnTurnSideChange.Invoke(isPlayerTurn);
        EnemyTurn();
    }

    public void EnemyTurn()
    {
        currentEnemyIndex = 0;
        TryPerformEnemyActions();
    }

    private void TryPerformEnemyActions()
    {
        if (currentEnemyIndex <= enemiesManager.enemies.Count)
        {
            enemiesManager.enemies[currentEnemyIndex].OnMyTurnEnd.AddListener(OnEnemyTurnEnded);
            enemiesManager.enemies[currentEnemyIndex].PerformTurn();
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
    private void EndEnemiesTurn()
    {
        isPlayerTurn = true;
        OnTurnSideChange.Invoke(isPlayerTurn);
        roundsPlayed++;
    }
}
