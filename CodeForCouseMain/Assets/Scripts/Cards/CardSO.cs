using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    [SerializeField] public ActionDataSO boundActionData;
    [SerializeField] public string cardDescription = "Default Card Text";

    [SerializeField] public int cost = 5;
}
