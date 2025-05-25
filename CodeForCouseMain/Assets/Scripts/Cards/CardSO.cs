using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] public bool canMoveNormal = true;
    [SerializeField, ShowIf(nameof(canMoveNormal))] public int cardNormalDistanceToMoveNormal = 1;
    [SerializeField] public bool canMoveDiagonal = true;
    [SerializeField, ShowIf(nameof(canMoveDiagonal))] public int cardNormalDistanceToMoveDiagonal = 1;

    [Header("Attack Settings")]
    [SerializeField] public bool canAttackNormal = true;
    [SerializeField, ShowIf(nameof(canAttackNormal))] public int cardNormalDistanceToAttackNormal = 1;
    [SerializeField] public bool canAttackDiagonal = true;
    [SerializeField, ShowIf(nameof(canAttackDiagonal))] public int cardNormalDistanceToAttackDiagonal = 1;

    [SerializeField] public string cardDescription = "Default Card Text";

    [SerializeField] public int cost = 5;
}
