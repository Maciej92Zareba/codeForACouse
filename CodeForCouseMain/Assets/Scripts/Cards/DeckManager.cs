using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<Card> allPlayableCards;
    [SerializeField] List<Card> startingDeck;
    public List<Card> deck;
    List<Card> drawPile;

    [SerializeField] GridController gridController;
    [SerializeField] Character playerCharacter;
    [SerializeField] EconomyManager economyManager;

    [Header("Hand Settings")]
    [SerializeField] GameObject activeCard;
    [SerializeField] TMP_Text handText;
    Card currentCardInHand;
    public UnityEvent cardDrawn;    
    public UnityEvent cardPlayed;    
    
    [Header("Card Selection Event Settings")]
    [HideInInspector] public bool isCardSelectionEventActive = false;
    [SerializeField] GameObject cardSelectionScreen;
    [SerializeField] TMP_Text[] cardTextsForSelectionEvent;
    [SerializeField] TMP_Text[] cardCostTextsForSelectionEvent;
    int numCardsToSelect = 3;
    List<Card> selectionEventCards;
    public UnityEvent cardSelectionOver;

    [Header("Card Removal Event SEttings")]
    [SerializeField] TMP_Text[] cardTextsForRemovalEvent;
    [SerializeField] GameObject[] cardGOForRemovalEvent;
    [SerializeField] TMP_Text removalCostText;
    [SerializeField] string removalCostTextString = "Removal Cost: ";
    [SerializeField] int removalCost = 5;

    private void Awake()
    {
        removalCostText.text = removalCostTextString + removalCost.ToString();
    }

    [Button]
    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            drawPile = new List<Card>(deck); // add reshuffle sound
        }
        
        cardSelectionOver.RemoveListener(DrawCard);
        int cardToDrawIndex = Random.Range(0, drawPile.Count);
        Card cardToDraw = drawPile[cardToDrawIndex];
        drawPile.Remove(cardToDraw);

        currentCardInHand = cardToDraw;

        handText.text = cardToDraw.cardSO.cardDescription;
        activeCard.gameObject.SetActive(true);

        cardDrawn.Invoke();
    }
    public void PlayCard()
    {
        GridPosition caller = playerCharacter.CharacterGridPosition;
        bool canMoveNormal = currentCardInHand.cardSO.canMoveNormal;
        int normalDistanceToMove = currentCardInHand.cardSO.cardNormalDistanceToMoveNormal;
        bool canMoveDiagonal = currentCardInHand.cardSO.canMoveDiagonal;
        int diagonalDistanceToMove = currentCardInHand.cardSO.cardNormalDistanceToMoveDiagonal;
        
        gridController.UpdateValidGridsToMove(caller, canMoveNormal, normalDistanceToMove, canMoveDiagonal, diagonalDistanceToMove); //pogadać z Maćkiem bo nie działa 

        activeCard.gameObject.SetActive(false);

        cardPlayed.Invoke();
    }

    public void GiveStartingCards()
    {
        deck = new List<Card>(startingDeck);
        drawPile = new List<Card>(deck);
    }

    public void GainCard(Card cardToAdd)
    {
        deck.Add(cardToAdd);
    }


    public void CardSelectionEvent()
    {
        isCardSelectionEventActive = true;

        cardSelectionScreen.SetActive(true);

        for (int i = 0; i < numCardsToSelect; i++)
        {
            int randomCardIndex = Random.Range(0, allPlayableCards.Count);
            Card card = allPlayableCards[randomCardIndex];

            cardTextsForSelectionEvent[i].text = card.cardSO.cardDescription;
            cardCostTextsForSelectionEvent[i].text = card.cardSO.cost.ToString();

            selectionEventCards.Add(card);
        }
        cardSelectionOver.AddListener(DrawCard);
    }

    public void CardSelected(int cardChosenIndex)
    {
        Card chosenCard = selectionEventCards[cardChosenIndex];

        if (chosenCard.cardSO.cost > economyManager.currencyOwned) { return; }

        foreach (Card card in selectionEventCards)
        {
            card.gameObject.SetActive(false);
        }

        isCardSelectionEventActive = false;
        cardSelectionOver.Invoke();
        economyManager.AddCurrency(-chosenCard.cardSO.cost);
        GainCard(chosenCard);
        ClearSelectionActionScreen();
    }

    public void SkipCardSelection()
    {
        ClearSelectionActionScreen();
    }
    private void ClearSelectionActionScreen()
    {
        selectionEventCards.Clear();
        cardSelectionScreen.SetActive(false);
        isCardSelectionEventActive = false; // zapytać Maćka czy da się to poprawić (na pewno się da xd) dla mnie to wygląda jakby powinno być eventem boolowym

        cardSelectionOver.Invoke();
    }
    public void CardRemovalEvent()
    {
        foreach (GameObject cardGO in cardGOForRemovalEvent)
        {
            cardGO.gameObject.SetActive(false);
        }

        // Add sorting deck

        for (int i = 0; i < deck.Count; i++)
        {
            Card card = deck[i];
            cardGOForRemovalEvent[i].SetActive(true);
            cardTextsForRemovalEvent[i].text = card.cardSO.cardDescription;
        }
    }
    public void RemoveCard(int index)
    {        
        if (removalCost > economyManager.currencyOwned) { return; }
        
        deck.RemoveAt(index);
        economyManager.AddCurrency(-removalCost);
        CardRemovalEvent();
    }
}
