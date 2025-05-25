using System;

public class GlobalActions : SingletonMonoBehaviour<GlobalActions>
{
	public event Action RestoreDefaultBoardLook = delegate {};
	public event Action<MovementData, GridPosition> UpdateValidGridsToMove = delegate {};
	public event Action<bool> OnTurnChange = delegate {};
	public event Action<AttackData, GridPosition> UpdateValidGridsToAttack = delegate {};
	public event Action<ActionDataSO> OnCardPlayed = delegate {};
	public event Action OnPlayerFinishedAttack = delegate {};
	public event Action OnEnemiesFinishedAttack = delegate {};

	public void NotifyOnRestoreDefaultBoardLook ()
	{
		RestoreDefaultBoardLook();
	}
	
	public void RequestUpdateValidGridsToMove (MovementData movementData, GridPosition caller)
	{
		UpdateValidGridsToMove(movementData, caller);
	}
	
	public void RequestUpdateValidGridsToAttack (AttackData attackData, GridPosition caller)
	{
		UpdateValidGridsToAttack(attackData, caller);
	}
	
	public void NotifyOnOnTurnChange (bool isPlayerTurn)
	{
		OnTurnChange(isPlayerTurn);
	}

	public void NotifyOnCardPlayed (ActionDataSO cardActionData)
	{
		OnCardPlayed(cardActionData);
	}
	
	public void NotifyOnPlayerFinishedAttack ()
	{
		OnPlayerFinishedAttack();
	}
	
	public void NotifyOnEnemiesFinishedAttack ()
	{
		OnEnemiesFinishedAttack();
	}
	
	protected override void Initialize ()
	{
		
	}
}
