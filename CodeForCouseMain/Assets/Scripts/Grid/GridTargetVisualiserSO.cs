using UnityEngine;

[CreateAssetMenu(fileName = "GridTargetVisualiserSO", menuName = "Scriptable Objects/GridTargetVisualiserSO")]
public class GridTargetVisualiserSO : ScriptableObject
{
    public Material defaultColor;
    public Material highlightedColor;
    public Material validMovementColor;
    public Material validAttackColor;
}
