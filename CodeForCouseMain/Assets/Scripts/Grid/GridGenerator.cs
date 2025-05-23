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

    private Transform cachedGridParent;
    
    [MenuItem("Tools/" + nameof(GridGenerator))]
    public static void ShowWindow ()
    {
        GetWindow<GridGenerator>().Show();
    }
    
    [Button]
    private void GenerateGrid ()
    {
        if (cachedGridParent != null)
        {
            DestroyImmediate(cachedGridParent);
        }
        
        cachedGridParent = new GameObject("GridParent").transform;
        float gridWidth = rowCount * gridDistance / 2;
        float gridHeight = columnCount * gridDistance / 2;

        float xLeftEdge = (gridDistance * rowCount / -2) + gridDistance / 2;
        float zTopEdge = (gridDistance * columnCount / -2) + gridDistance / 2;

        for (int i = 0; i < rowCount; i++)
        {
            float xPosition = i * gridDistance + xLeftEdge;
            
            for (int j = 0; j < columnCount; j++)
            {
                float zPosition = j * gridDistance + zTopEdge;
                Vector3 gridPosition = new (xPosition, 0.0f, zPosition);
                Transform gridTarget = Instantiate(gridTargetPrefab, cachedGridParent).transform;
                gridTarget.name = $"GridTarget_{i}_{j}";
                gridTarget.position = gridPosition;
            }
        }
    }
}
