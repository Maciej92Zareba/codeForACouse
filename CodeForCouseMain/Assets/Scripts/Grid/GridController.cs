using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GridController : SerializedMonoBehaviour
{
	[field: SerializeField, TableMatrix(DrawElementMethod = nameof(CustomGrid2dArrayDraw))] public GridTarget[,] BoardNodes2dArray { get; private set; }

	public void InitializeGridController (int gridRowCount, int gridColumnCount)
	{
		BoardNodes2dArray = new GridTarget[gridRowCount, gridColumnCount];
	}
	
	private static GridTarget CustomGrid2dArrayDraw(Rect rect, GridTarget value)
	{
		EditorGUIUtility.labelWidth = 50.0f;
		string label = value != null ? value.name : "Null";
		value = (GridTarget)EditorGUI.ObjectField(rect, label, value, typeof(GridTarget));
		return value;
	}
}
