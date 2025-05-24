using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent OnMyTurnEnd;    

    public void PerformTurn()
    {
        //do something

        OnMyTurnEnd.Invoke();
    }
}
