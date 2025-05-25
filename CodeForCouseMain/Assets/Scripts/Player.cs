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
        GlobalActions.Instance.RequestUpdateValidGridsToMove(BoundActionData.movementData, CharacterGridPosition);
    }
}
