using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseBoardObject : MonoBehaviour
{
    public void InitializeBoardObject (GridTarget gridTarget)
    {
        PlacedOnGrid = gridTarget;
        PlacedOnGrid.IsObstructed = true;
        gridTarget.PlaceObjectOnGrid = this;
    }

    protected void ResetPlacedObject ()
    {
        PlacedOnGrid.IsObstructed = false;
        PlacedOnGrid.PlaceObjectOnGrid = null;
    }

    protected virtual void OnDie ()
    {
        
    }
    
    [ShowInInspector, ReadOnly] public GridTarget PlacedOnGrid { get; protected set; }
    public abstract void ReactOnGettingAttacked (int damage);
}
