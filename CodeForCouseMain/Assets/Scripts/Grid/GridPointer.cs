using UnityEngine;

public class GridPointer : MonoBehaviour
{
    [SerializeField] private LayerMask targetsLayer;
    [SerializeField] private Camera mainCamera;

    private DefaultInputActions defaultInputActions;
    private GridTarget cachedGridTarget;

    private void Awake ()
    {
        defaultInputActions = new();
        defaultInputActions.DefaultActionsMap.Enable();
    }

    private void OnDestroy ()
    {
        defaultInputActions.DefaultActionsMap.Disable();
    }

    private void FixedUpdate ()
    {
        UpdateSelectedGridTarget();
    }

    private void UpdateSelectedGridTarget ()
    {
        Vector2 mousePosition = defaultInputActions.DefaultActionsMap.MousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetsLayer) == true)
        {
            GridTarget gridTarget = hit.collider.GetComponent<GridTarget>();
            
            if (gridTarget != cachedGridTarget)
            {
                TrySetPrevious();
                cachedGridTarget = gridTarget;

                if (cachedGridTarget != null)
                {
                    cachedGridTarget.SetState(GridTargetState.HIGHLIGHTED);
                }
            }
        }
        else
        {
            TrySetPrevious();
            cachedGridTarget = null;
        }
    }

    private void TrySetPrevious ()
    {
        if (cachedGridTarget != null)
        {
            cachedGridTarget.RestorePreviousState();
        }
    }
}
