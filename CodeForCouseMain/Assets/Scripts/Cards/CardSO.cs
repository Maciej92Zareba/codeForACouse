using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private bool canMoveNormal = true;
    [SerializeField, ShowIf(nameof(canMoveNormal))] private int cardNormalDistanceToMoveNormal = 1;
    [SerializeField] private bool canMoveDiagonal = true;
    [SerializeField, ShowIf(nameof(canMoveDiagonal))] private int cardNormalDistanceToMoveDiagonal = 1;

    [Header("Attack Settings")]
    [SerializeField] private bool canAttackNormal = true;
    [SerializeField, ShowIf(nameof(canAttackNormal))] private int cardNormalDistanceToAttackNormal = 1;
    [SerializeField] private bool canAttackDiagonal = true;
    [SerializeField, ShowIf(nameof(canAttackDiagonal))] private int cardNormalDistanceToAttackDiagonal = 1;

    [SerializeField] public string cardDescription = "Default Card Text";

    [SerializeField] private int cost = 5;
}
