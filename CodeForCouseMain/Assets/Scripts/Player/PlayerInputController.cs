using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private LayerMask targetsLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Character player;

    private DefaultInputActions defaultInputActions;
    private GridTarget cachedGridTarget;
    private Action cachedPlayerArrivedAtDestinationAction;
    private Action<InputAction.CallbackContext> cachedMouseButtonReleasedAction;

    private void Awake ()
    {
        defaultInputActions = new();
        cachedPlayerArrivedAtDestinationAction = HandlePlayerArrivedAtDestination;
        cachedMouseButtonReleasedAction = HandleMouseButtonReleased;
    }

    private void Start ()
    {
        GlobalActions.Instance.OnTurnChange += HandleTurnChanged;
    }

    private void HandleTurnChanged (bool isPlayerTurn)
    {
        enabled = isPlayerTurn;

        if (isPlayerTurn == true)
        {
            GlobalActions.Instance.RequestUpdateValidGridsToMove(player.boundCharacterData.movementData, player.CharacterGridPosition);
        }
    }

    private void OnEnable ()
    {
        defaultInputActions.DefaultActionsMap.Enable();
        defaultInputActions.DefaultActionsMap.MouseClickRelease.performed += cachedMouseButtonReleasedAction;
    }

    private void OnDisable ()
    {
        defaultInputActions.DefaultActionsMap.Disable();
        defaultInputActions.DefaultActionsMap.MouseClickRelease.performed -= cachedMouseButtonReleasedAction;
    }

    private void HandleMouseButtonReleased (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (cachedGridTarget != null && cachedGridTarget.CurrentState == GridTargetState.VALID_MOVEMENT)
            {
                player.OnArrivedAtDestination += cachedPlayerArrivedAtDestinationAction;
                player.SetCharacterDestination(cachedGridTarget.PlacedObjectParent.position, cachedGridTarget.BoundGridPosition.RowIndex, cachedGridTarget.BoundGridPosition.ColumnIndex);
                cachedGridTarget.IsObstructed = true;
                GlobalActions.Instance.NotifyOnRestoreDefaultBoardLook();
            }
            
            if (cachedGridTarget != null && cachedGridTarget.CurrentState == GridTargetState.VALID_ATTACK)
            {
                //PerformAttack
                GlobalActions.Instance.NotifyOnRestoreDefaultBoardLook();
            }
        }
    }

    private void HandlePlayerArrivedAtDestination ()
    {
        player.OnArrivedAtDestination -= cachedPlayerArrivedAtDestinationAction;
        GlobalActions.Instance.RequestUpdateValidGridsToAttack(player.boundCharacterData.attackData, player.CharacterGridPosition);
    }

    private void OnDestroy ()
    {
        GlobalActions.Instance.OnTurnChange -= HandleTurnChanged;
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
                        cachedGridTarget.SetHighlightState(true);
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
            cachedGridTarget.SetHighlightState(false);
        }
    }
}
