using Sirenix.OdinInspector;
using UnityEngine;

public class GridTarget : MonoBehaviour
{
	[SerializeField] private MeshRenderer gridMesh;
	[SerializeField] private GridTargetVisualiserSO boundGridTargetVisualiserSO;
	[field: SerializeField] public GridPosition BoundGripPosition { get; set; }
	[field: SerializeField] public bool IsObstructed { get; set; }
	[field: SerializeField] public Transform PlacedObjectParent { get; private set; }

	[ShowInInspector, ReadOnly] private GridTargetState currentState = GridTargetState.DEFAULT;
	[ShowInInspector, ReadOnly] private GridTargetState previousState = GridTargetState.DEFAULT;

	public void SetState (GridTargetState newState)
	{
		if (currentState != newState)
		{
			previousState = currentState;
			currentState = newState;
			SetLookBasedOnCurrentState();
		}
	}

	public void RestorePreviousState ()
	{
		if (currentState != previousState)
		{
			currentState = previousState;
			SetLookBasedOnCurrentState();
		}
	}

	private void SetLookBasedOnCurrentState ()
	{
		Material selectedMaterial;
		
		switch (currentState)
		{
			case GridTargetState.DEFAULT:
				selectedMaterial = boundGridTargetVisualiserSO.defaultColor;
				break;
			case GridTargetState.HIGHLIGHTED:
				selectedMaterial = boundGridTargetVisualiserSO.highlightedColor;
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