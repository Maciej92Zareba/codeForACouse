using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GridController : SerializedMonoBehaviour
{
	[field: SerializeField, TableMatrix(DrawElementMethod = nameof(CustomGrid2dArrayDraw))] public GridTarget[,] GridTargets2dArray { get; private set; }
	[SerializeField] private Character player;
	// [SerializeField] private bool canMoveNormal = true;
	// [SerializeField, ShowIf(nameof(canMoveNormal))] private int playerNormaDistanceToMoveNormal = 2;
	// [SerializeField] private bool canMoveDiagonal = true;
	// [SerializeField, ShowIf(nameof(canMoveDiagonal))] private int playerNormaDistanceToMoveDiagonal = 1;
	
	private List<GridTarget> validGridTargets = new();

	public void InitializeGridController (int gridRowCount, int gridColumnCount)
	{
		GridTargets2dArray = new GridTarget[gridRowCount, gridColumnCount];
	}

	private void Start ()
	{
		MovePlayerToGrid(0, 0);
	}

	[Button]
	public void UpdateValidGridsToMove (bool canMoveNormal, int normalDistanceToMove, bool canMoveDiagonal, int diagonalDistanceToMove)
	{
		RestoreDefaultLook();
		validGridTargets.Clear();
		AddNormalMovementTargets();
		AddDiagonalMovementTargets();

		for (int i = 0; i < validGridTargets.Count; i++)
		{
			validGridTargets[i].SetState(GridTargetState.VALID);
		}

		void AddNormalMovementTargets ()
		{
			if (canMoveNormal == true)
			{
				for (int i = 0; i < normalDistanceToMove; i++)
				{
					GridPosition playerGridPosition = player.CharacterGridPosition;
					int moveDistance = i + 1;
					TryAddGridTarget(playerGridPosition.RowIndex + moveDistance, playerGridPosition.ColumnIndex);
					TryAddGridTarget(playerGridPosition.RowIndex - moveDistance, playerGridPosition.ColumnIndex);
					TryAddGridTarget(playerGridPosition.RowIndex, playerGridPosition.ColumnIndex + moveDistance);
					TryAddGridTarget(playerGridPosition.RowIndex, playerGridPosition.ColumnIndex - moveDistance);
				}
			}
		}

		void AddDiagonalMovementTargets ()
		{
			if (canMoveDiagonal == true)
			{
				for (int i = 0; i < diagonalDistanceToMove; i++)
				{
					GridPosition playerGridPosition = player.CharacterGridPosition;
					int moveDistance = i + 1;
					TryAddGridTarget(playerGridPosition.RowIndex + moveDistance, playerGridPosition.ColumnIndex + moveDistance);
					TryAddGridTarget(playerGridPosition.RowIndex + moveDistance, playerGridPosition.ColumnIndex - moveDistance);
					TryAddGridTarget(playerGridPosition.RowIndex - moveDistance, playerGridPosition.ColumnIndex + moveDistance);
					TryAddGridTarget(playerGridPosition.RowIndex - moveDistance, playerGridPosition.ColumnIndex - moveDistance);
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

	public void MovePlayerToGrid (GridPosition gridPosition)
	{
		MovePlayerToGrid(gridPosition.RowIndex, gridPosition.ColumnIndex);
	}
	
	private void MovePlayerToGrid (int rowIndex, int columIndex)
	{
		//TODO if 0,0 is obstructed on start it will be problem
		GridTargets2dArray[player.CharacterGridPosition.RowIndex, player.CharacterGridPosition.ColumnIndex].IsObstructed = false;
		player.SetCharacterDestination(GridTargets2dArray[rowIndex, columIndex].PlacedObjectParent.position, rowIndex, columIndex);
		GridTargets2dArray[rowIndex, columIndex].IsObstructed = true;
		RestoreDefaultLook();
	}

	[Button]
	private void ShowValidGridDestination ()
	{
		
	}

	[Button]
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
		value = (GridTarget)EditorGUI.ObjectField(rect, label, value, typeof(GridTarget));
		return value;
	}
}