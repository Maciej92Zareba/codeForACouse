using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Health Settings")]
    [SerializeField] int healthPoints = 1;

    [Header("Movement Settings")]
    [SerializeField] private bool canMoveNormal = true;
    [SerializeField, ShowIf(nameof(canMoveNormal))] private int enemyNormalDistanceToMoveNormal = 2;
    [SerializeField] private bool canMoveDiagonal = true;
    [SerializeField, ShowIf(nameof(canMoveDiagonal))] private int enemyNormalDistanceToMoveDiagonal = 1;

    [Header("Attack Settings")]
    [SerializeField] private bool canAttackNormal = true;
    [SerializeField, ShowIf(nameof(canAttackNormal))] private int enemyNormalDistanceToAttackNormal = 1;
    [SerializeField] private bool canAttackDiagonal = true;
    [SerializeField, ShowIf(nameof(canAttackDiagonal))] private int enemyNormalDistanceToAttackDiagonal = 1;
}
