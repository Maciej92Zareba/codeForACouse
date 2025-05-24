using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Scriptable Objects/CharacterDataSO")]
public class CharacterDataSO : ScriptableObject
{
    [Header("Health Settings")]
    [SerializeField] int healthPoints = 1;

    [Header("Movement Settings")]
    [SerializeField] public MovementData movementData;

    [Header("Attack Settings")]
    [SerializeField] public AttackData attackData;
}

[System.Serializable]
public class MovementData
{
    [field: SerializeField] public bool CanMoveNormal { get; private set; } = true;
    [field: SerializeField, ShowIf(nameof(CanMoveNormal))] public int DistanceToMoveNormal { get; private set; } = 2;
    [field: SerializeField] public bool CanMoveDiagonal { get; private set; } = true;
    [field: SerializeField, ShowIf(nameof(CanMoveDiagonal))] public int DistanceToMoveDiagonal { get; private set; } = 1;
}

[System.Serializable]
public class AttackData
{
    [field: SerializeField] public bool CanAttackNormal { get; private set; } = true;
    [field: SerializeField, ShowIf(nameof(CanAttackNormal))] public int NormalDistanceToAttack { get; private set; } = 2;
    [field: SerializeField] public bool CanAttackDiagonal { get; private set; } = true;
    [field: SerializeField, ShowIf(nameof(CanAttackDiagonal))] public int DiagonalDistanceAttack { get; private set; } = 1;
}
