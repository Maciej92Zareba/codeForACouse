using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<Card> allPlayableCards;
    [SerializeField] List<Card> startingDeck;
    List<Card> deck;
    List<Card> drawPile;

    [HideInInspector] public bool isCardSelectionEventActive = false;

    [SerializeField] GridController gridController;

    [Header("Hand Settings")]
    [SerializeField] GameObject hand;
    [SerializeField] TMP_Text handText;
    Card currentCardInHand;
    
    [Header("Card Selection Event Settings")]
    [SerializeField] GameObject cardSelectionScreen;
    [SerializeField] TMP_Text[] cardTexts;
    int numCardsToSelect = 3;
    List<Card> selectionEventCards;
    float[] cardSelectionEventSpawnXPos = { -1.2f, 0f, 1.2f };
    
    [Button]
    public void DrawCard()
    {
        int cardToDrawIndex = Random.Range(0, drawPile.Count);
        Card cardToDraw = drawPile[cardToDrawIndex];
        drawPile.Remove(cardToDraw);

        currentCardInHand = cardToDraw;

        handText.text = cardToDraw.cardSO.cardDescription;
    }
    public void PlayCard()
    {

    }

    public void GiveStartingCards()
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

    public void CardSelectionEvent()
    {
        isCardSelectionEventActive = true;

        cardSelectionScreen.SetActive(true);

        for (int i = 0; i < numCardsToSelect; i++)
        {
            int randomCardIndex = Random.Range(0, allPlayableCards.Count);
            Card card = allPlayableCards[randomCardIndex];

            cardTexts[i].text = card.cardSO.cardDescription;

            selectionEventCards.Add(card);
        }
    }

    public void CardSelected()
    {
        isCardSelectionEventActive = false;
        foreach (Card card in selectionEventCards)
        {
            card.gameObject.SetActive(false);
        }
        selectionEventCards.Clear();
    }

    public void CardSelected(int cardChosen)
    {
        deck.Add(selectionEventCards[cardChosen]);
        selectionEventCards.Clear();
        cardSelectionScreen.SetActive(false);
        isCardSelectionEventActive = false; // zapytać Maćka czy da się to poprawić (na pewno się da xd) dla mnie to wygląda jakby powinno być eventem boolowym
    }

    public void SkipCardSelection()
    {
        selectionEventCards.Clear();
        cardSelectionScreen.SetActive(false);
        isCardSelectionEventActive = false;
    }
}
