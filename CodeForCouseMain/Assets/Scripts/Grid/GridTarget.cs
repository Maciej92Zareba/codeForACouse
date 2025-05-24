using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GridTarget : MonoBehaviour
{
	[SerializeField] private MeshRenderer gridMesh;
	[SerializeField] private GridTargetVisualiserSO boundGridTargetVisualiserSO;
	[field: SerializeField] public GridPosition BoundGridPosition { get; set; }
	[field: SerializeField] public bool IsObstructed { get; set; }
	[field: SerializeField] public Transform PlacedObjectParent { get; private set; }
	
	[ShowInInspector, ReadOnly] public GridTargetState CurrentState { get; private set; } = GridTargetState.DEFAULT;

	private bool isHighlighted;

	public void SetHighlightState (bool isOn)
	{
		if (isHighlighted != isOn)
		{
			isHighlighted = isOn;
			
			if (isHighlighted == true)
			{
				gridMesh.sharedMaterial = boundGridTargetVisualiserSO.highlightedColor;
			}
			else
			{
				SetLookBasedOnCurrentState();
			}
		}
	}
	
	public void SetState (GridTargetState newState)
	{
		if (CurrentState != newState)
		{
			CurrentState = newState;
			SetLookBasedOnCurrentState();
		}
	}

	private void SetLookBasedOnCurrentState ()
	{
		Material selectedMaterial;
		
		switch (CurrentState)
		{
			case GridTargetState.DEFAULT:
				selectedMaterial = boundGridTargetVisualiserSO.defaultColor;
				break;
			case GridTargetState.VALID:
				selectedMaterial = boundGridTargetVisualiserSO.validColor;
				break;
			default:
				selectedMaterial = boundGridTargetVisualiserSO.defaultColor;
				break;
		}

		gridMesh.sharedMaterial = selectedMaterial;
	}
}