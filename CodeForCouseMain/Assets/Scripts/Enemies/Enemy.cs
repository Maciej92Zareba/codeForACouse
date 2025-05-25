using System;
using UnityEngine;

public class Enemy : Character
{
    public event Action OnFinishedTurn = delegate {};
    public event Action OnEnemyDied = delegate {};

    public void PerformTurn()
    {
        //do something
        Debug.Log("Performed turn", gameObject);
        OnFinishedTurn();
    }

    protected override void OnDie ()
    {
        Debug.Log("OnEnemyDied");
        boundAnimator.SetBool(deadAnimation, true);
        ResetPlacedObject();
        boundAgent.enabled = false;
        OnEnemyDied();
    }
}
