using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class GridPosition
{
	[field: SerializeField, ReadOnly] public int RowIndex { get; private set; }
	[field: SerializeField, ReadOnly] public int ColumnIndex { get; private set; }

	public GridPosition (int row, int column)
	{
		SetGridPosition(row, column);
	}
	
	public void SetGridPosition (int row, int column)
	{
		RowIndex = row;
		ColumnIndex = column;
	}
}