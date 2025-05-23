using UnityEngine;

[CreateAssetMenu(fileName = "GridTargetVisualiserSO", menuName = "Scriptable Objects/GridTargetVisualiserSO")]
public class GridTargetVisualiserSO : ScriptableObject
{
    public Color defaultColor = Color.gray;
    public Color highlightedColor = Color.green;
    public Color validColor = Color.blue;
}
