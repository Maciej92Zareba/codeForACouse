using UnityEngine;

public class GridRaycastTarget : MonoBehaviour
{
    [SerializeField] private Collider boundCollider;
    [SerializeField] public GridTarget boundGridTarget;

    public void SetColliderState (bool isEnable)
    {
        boundCollider.enabled = isEnable;
    }
}
