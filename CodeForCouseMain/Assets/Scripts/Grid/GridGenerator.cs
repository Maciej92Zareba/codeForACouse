using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class GridGenerator : OdinEditorWindow
{
    [SerializeField] private GridTarget gridTargetPrefab;
    [SerializeField] private int rowCount = 5;
    [SerializeField] private int columnCount = 5;
    [SerializeField] private float gridDistance = 2.0f;

    private GameObject cachedGrid;
    
    [MenuItem("Tools/" + nameof(GridGenerator))]
    public static void ShowWindow ()
    {
        GetWindow<GridGenerator>().Show();
    }
    
    [Button]
    private void GenerateGrid ()
    {
        if (cachedGrid != null)
        {
            DestroyImmediate(cachedGrid);
        }
        
        cachedGrid = new GameObject("GridParent");
        Transform gridParent = cachedGrid.transform;
        GridController gridController = cachedGrid.AddComponent<GridController>();
        gridController.InitializeGridController(rowCount,columnCount);

        float xLeftEdge = (gridDistance * rowCount / -2) + gridDistance / 2;
        float zTopEdge = (gridDistance * columnCount / -2) + gridDistance / 2;

        for (int i = 0; i < rowCount; i++)
        {
            float xPosition = i * gridDistance + xLeftEdge;
            
            for (int j = 0; j < columnCount; j++)
            {
                float zPosition = j * gridDistance + zTopEdge;
                Vector3 gridPosition = new (xPosition, 0.0f, zPosition);
                GridTarget gridTarget = (GridTarget)PrefabUtility.InstantiatePrefab(gridTargetPrefab, gridParent);
                gridTarget.BoundGripPosition = new GridPosition(i,j);
                Transform gridTargetTransform = gridTarget.transform;
                gridTargetTransform.name = $"GT_{i}_{j}";
                gridTargetTransform.position = gridPosition;
                gridController.GridTargets2dArray[i, j] = gridTarget;
            }
        }
    }
}
