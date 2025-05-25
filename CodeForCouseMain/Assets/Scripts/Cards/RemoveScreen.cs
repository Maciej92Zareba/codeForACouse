using System.Collections.Generic;
using UnityEngine;

public class RemoveScreen : MonoBehaviour
{
    [SerializeField] DeckManager deckManager;
    [SerializeField] GameObject cardUIElement;

    List<Card> deck = new List<Card>();

    private void OnEnable()
    {
        foreach (Card card in deck)
        {
            Destroy(card);
        }
        
        deck = deckManager.deck;
        foreach (Card card in deck)
        {
            Instantiate(cardUIElement, this.transform);
        }
    }
}
