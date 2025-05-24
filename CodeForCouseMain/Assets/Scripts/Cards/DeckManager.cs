using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<Card> startingDeck;
    List<Card> deck;
    List<Card> drawPile;

    [SerializeField] GridController gridController;

    [SerializeField] Transform cardTransform;
    
    private void Start()
    {
        GiveStartingCards();
        DrawCard();
    }

    [Button]
    public void DrawCard()
    {
        int cardToDrawIndex = Random.Range(0, drawPile.Count);
        Card cardToDraw = drawPile[cardToDrawIndex];
        drawPile.Remove(cardToDraw);

        Card cardDrawn = Instantiate(cardToDraw, cardTransform);

        cardDrawn.Init(this, gridController);
    }

    private void GiveStartingCards()
    {
        deck = new List<Card>(startingDeck);
        drawPile = new List<Card>(startingDeck);
    }

    public void GainCard(Card cardToAdd)
    {
        deck.Add(cardToAdd);
    }

    public void RemoveCard(Card cardToRemove)
    {
        deck.Remove(cardToRemove);
    }
}
