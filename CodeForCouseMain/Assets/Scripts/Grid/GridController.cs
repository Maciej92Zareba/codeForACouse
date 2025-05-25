using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GridController : SerializedMonoBehaviour
{
	[field: SerializeField, TableMatrix(DrawElementMethod = nameof(CustomGrid2dArrayDraw))] public GridTarget[,] GridTargets2dArray { get; private set; }

	private List<GridTarget> validGridTargets = new();

	#if UNITY_EDITOR
	public void InitializeGridController (int gridRowCount, int gridColumnCount)
	{
		GridTargets2dArray = new GridTarget[gridRowCount, gridColumnCount];
	}
#endif

	private void UpdateValidGridsToMove (MovementData movementData, GridPosition caller)
	{
		//TODO add ignore obstructed
		UpdateValidGridsToMove(caller, movementData.CanMoveNormal, movementData.DistanceToMoveNormal, 
							   movementData.CanMoveDiagonal, movementData.DistanceToMoveDiagonal);
		
		for (int i = 0; i < validGridTargets.Count; i++)
		{
			validGridTargets[i].SetState(GridTargetState.VALID_MOVEMENT);
		}
	}
	
	private void UpdateValidGridsToAttack (AttackData attackData, GridPosition caller)
	{
		//TODO add dont ignore obstructed
		UpdateValidGridsToMove(caller, attackData.CanAttackNormal, attackData.NormalDistanceToAttack, 
							   attackData.CanAttackDiagonal, attackData.DiagonalDistanceAttack);
		
		for (int i = 0; i < validGridTargets.Count; i++)
		{
			validGridTargets[i].SetState(GridTargetState.VALID_ATTACK);
		}
	}
	
	[Button]
	public void UpdateValidGridsToMove (GridPosition caller, bool canNormal, int normalDistance, bool canDiagonal, int diagonalDistance)
	{
		RestoreDefaultLook();
		validGridTargets.Clear();
		AddNormalMovementTargets();
		AddDiagonalMovementTargets();
		

		void AddNormalMovementTargets ()
		{
			if (canNormal == true)
			{
				for (int i = 0; i < normalDistance; i++)
				{
					int moveDistance = i + 1;
					TryAddGridTarget(caller.RowIndex + moveDistance, caller.ColumnIndex);
					TryAddGridTarget(caller.RowIndex - moveDistance, caller.ColumnIndex);
					TryAddGridTarget(caller.RowIndex, caller.ColumnIndex + moveDistance);
					TryAddGridTarget(caller.RowIndex, caller.ColumnIndex - moveDistance);
				}
			}
		}

		void AddDiagonalMovementTargets ()
		{
			if (canDiagonal == true)
			{
				for (int i = 0; i < diagonalDistance; i++)
				{
					int moveDistance = i + 1;
					TryAddGridTarget(caller.RowIndex + moveDistance, caller.ColumnIndex + moveDistance);
					TryAddGridTarget(caller.RowIndex + moveDistance, caller.ColumnIndex - moveDistance);
					TryAddGridTarget(caller.RowIndex - moveDistance, caller.ColumnIndex + moveDistance);
					TryAddGridTarget(caller.RowIndex - moveDistance, caller.ColumnIndex - moveDistance);
				}
			}
		}
		
		void TryAddGridTarget (int row, int column)
		{
			if (IsOutsideOfGridLength(row, column) == false)
			{
				GridTarget target = GridTargets2dArray[row, column];

				if (target.IsObstructed == false && validGridTargets.Contains(target) == false)
				{
					validGridTargets.Add(target);
				}
			}
		}
	}

	private void Start ()
	{
		GlobalActions.Instance.RestoreDefaultBoardLook += RestoreDefaultLook;
		GlobalActions.Instance.UpdateValidGridsToMove += UpdateValidGridsToMove;
		GlobalActions.Instance.UpdateValidGridsToAttack += UpdateValidGridsToAttack;
	}

	private void OnDestroy ()
	{
		if (GlobalActions.Instance != null)
		{
			GlobalActions.Instance.RestoreDefaultBoardLook -= RestoreDefaultLook;
			GlobalActions.Instance.UpdateValidGridsToMove -= UpdateValidGridsToMove;
			GlobalActions.Instance.UpdateValidGridsToAttack -= UpdateValidGridsToAttack;
		}
	}

	private void RestoreDefaultLook ()
	{
		for (int i = 0; i < GridTargets2dArray.GetLength(0); i++)
		{
			for (int j = 0; j < GridTargets2dArray.GetLength(1); j++)
			{
				GridTargets2dArray[i,j].SetState(GridTargetState.DEFAULT);
			}
		}
	}
	
	private bool IsOutsideOfGridLength (int row, int column)
	{
		return row < 0 || column < 0 || GridTargets2dArray.GetLength(0) <= row || GridTargets2dArray.GetLength(1) <= column;
	}
	
	private static GridTarget CustomGrid2dArrayDraw(Rect rect, GridTarget value)
	{
		EditorGUIUtility.labelWidth = 50.0f;
		string label = value != null ? value.name : "Null";
		value = (GridTarget)EditorGUI.ObjectField(rect, label, value, typeof(GridTarget), true);
		return value;
	}
}