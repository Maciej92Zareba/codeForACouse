public class Player : Character
{
    private void Start ()
    {
        GlobalActions.Instance.OnCardPlayed += ChangePlayerCard;
    }

    private void OnDestroy ()
    {
        GlobalActions.Instance.OnCardPlayed -= ChangePlayerCard;
    }

    private void ChangePlayerCard (ActionDataSO dataSO)
    {
        BoundActionData = dataSO;
        GlobalActions.Instance.RequestUpdateValidGridsToMove(BoundActionData.movementData, PlacedOnGrid.BoundGridPosition);
    }
    
    protected override void OnDie ()
    {
        //SendEvent player died end game
    }
}
