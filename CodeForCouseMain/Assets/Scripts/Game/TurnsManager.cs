using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] EconomyManager economyManager;

    [HideInInspector] public bool isPlayerTurn = true;
    [HideInInspector] public int roundsPlayed = 0;
    
    [SerializeField] int currecnyToAddAfterTurnEnd = 10;

    private void Awake()
    {
        StartCoroutine(DelayedStartFirstPlayerTurn());
    }

    private void Start ()
    {
        GlobalActions.Instance.OnPlayerFinishedAttack += EndPlayerTurn;
        GlobalActions.Instance.OnEnemiesFinishedAttack += EndEnemiesTurn;
    }

    private void OnDestroy ()
    {
        GlobalActions.Instance.OnPlayerFinishedAttack -= EndPlayerTurn;
        GlobalActions.Instance.OnEnemiesFinishedAttack -= EndEnemiesTurn;
    }
    
    private IEnumerator DelayedStartFirstPlayerTurn ()
    {
        yield return null;
        GlobalActions.Instance.NotifyOnOnTurnChange(isPlayerTurn);
    }

    [Button]
    private void EndPlayerTurn() 
    {
        isPlayerTurn = false;
        //TODO move from here
        economyManager.AddCurrency(currecnyToAddAfterTurnEnd);
        GlobalActions.Instance.NotifyOnOnTurnChange(isPlayerTurn);
    }
    
    public void EndEnemiesTurn()
    {
        roundsPlayed++;
        isPlayerTurn = true;
        GlobalActions.Instance.NotifyOnOnTurnChange(isPlayerTurn);
        //TryPerformEnemyActions();
    }
}
