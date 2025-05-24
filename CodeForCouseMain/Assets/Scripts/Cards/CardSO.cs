using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private bool canMoveNormal = true;
    [SerializeField, ShowIf(nameof(canMoveNormal))] private int enemyNormalDistanceToMoveNormal = 1;
    [SerializeField] private bool canMoveDiagonal = true;
    [SerializeField, ShowIf(nameof(canMoveDiagonal))] private int enemyNormalDistanceToMoveDiagonal = 1;

    [Header("Attack Settings")]
    [SerializeField] private bool canAttackNormal = true;
    [SerializeField, ShowIf(nameof(canAttackNormal))] private int enemyNormalDistanceToAttackNormal = 1;
    [SerializeField] private bool canAttackDiagonal = true;
    [SerializeField, ShowIf(nameof(canAttackDiagonal))] private int enemyNormalDistanceToAttackDiagonal = 1;

    [SerializeField] private int cost = 5;
}
