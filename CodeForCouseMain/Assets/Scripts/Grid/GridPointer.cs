using UnityEngine;
using UnityEngine.InputSystem;

public class GridPointer : MonoBehaviour
{
    [SerializeField] private LayerMask targetsLayer;
    [SerializeField] private Camera mainCamera;

    private DefaultInputActions defaultInputActions;
    private GridTarget cachedGridTarget;

    private void Awake ()
    {
        defaultInputActions = new();

    }

    private void OnEnable ()
    {
        defaultInputActions.DefaultActionsMap.Enable();
        defaultInputActions.DefaultActionsMap.MouseClickRelease.performed += HandleMouseButtonClicked;
    }

    private void OnDisable ()
    {
        defaultInputActions.DefaultActionsMap.Disable();
        defaultInputActions.DefaultActionsMap.MouseClickRelease.performed -= HandleMouseButtonClicked;
    }

    private void HandleMouseButtonClicked (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Button clicked");
        }
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
            GridRaycastTarget gridRaycastTarget = hit.collider.GetComponent<GridRaycastTarget>();

            if (gridRaycastTarget != null)
            {
                if (gridRaycastTarget.boundGridTarget != cachedGridTarget)
                {
                    TrySetPrevious();
                    cachedGridTarget = gridRaycastTarget.boundGridTarget;

                    if (cachedGridTarget != null)
                    {
                        cachedGridTarget.SetState(GridTargetState.HIGHLIGHTED);
                    }
                }
            }
            else
            {
                TrySetPreviousStateAndNull();
            }
        }
        else
        {
            TrySetPreviousStateAndNull();
        }
    }

    private void TrySetPreviousStateAndNull ()
    {
        TrySetPrevious();
        cachedGridTarget = null;
    }

    private void TrySetPrevious ()
    {
        if (cachedGridTarget != null)
        {
            cachedGridTarget.RestorePreviousState();
        }
    }
}
