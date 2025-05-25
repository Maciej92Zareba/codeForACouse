using System;

public class GlobalActions : SingletonMonoBehaviour<GlobalActions>
{
	public event Action RestoreDefaultBoardLook = delegate {};
	public event Action<MovementData, GridPosition> UpdateValidGridsToMove = delegate {};
	public event Action<bool> OnTurnChange = delegate {};
	public event Action<AttackData, GridPosition> UpdateValidGridsToAttack = delegate {};

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
	
	protected override void Initialize ()
	{
		
	}
}
