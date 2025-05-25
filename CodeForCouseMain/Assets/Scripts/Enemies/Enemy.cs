using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnFinishedTurn = delegate {};

    public void PerformTurn()
    {
        //do something
        Debug.Log("Performed turn", gameObject);
        OnFinishedTurn();
    }
}
