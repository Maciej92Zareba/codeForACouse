using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DeckManager : MonoBehaviour
{
    [SerializeField] EconomyManager economyManager;

    [SerializeField] List<Card> allPlayableCards;
    [SerializeField] List<Card> startingDeck;
    [HideInInspector] public List<Card> deck;
    [HideInInspector] public List<Card> drawPile;

    [Header("Hand Settings")]
    [SerializeField] GameObject activeCard;
    [SerializeField] TMP_Text handText;
    Card currentCardInHand;
    
    [FormerlySerializedAs("isCardSelectionEventActive")]
    [Header("Card Selection Event Settings")]
    [HideInInspector] public bool isCardSelectionInProgress = false;
    [SerializeField] GameObject cardSelectionScreen;
    [SerializeField] TMP_Text[] cardTextsForSelectionEvent;
    [SerializeField] TMP_Text[] cardCostTextsForSelectionEvent;
    int numCardsToSelect = 3;
    List<Card> selectionEventCards;

    [Header("Card Removal Event SEttings")]
    [SerializeField] TMP_Text[] cardTextsForRemovalEvent;
    [SerializeField] GameObject[] cardGOForRemovalEvent;
    [SerializeField] TMP_Text removalCostText;
    [SerializeField] int removalCost = 5;

    [Header("Events")]
    public UnityEvent cardPlayed;    
    public UnityEvent cardSelectionOver;
    public UnityEvent drawPileChanged;

    private void Awake()
    {
        removalCostText.text += removalCost.ToString();
        GiveStartingCards();
    }

    private void Start ()
    {
        GlobalActions.Instance.OnTurnChange += HandleTurnChanged;
    }

    private void OnDestroy ()
    {
        GlobalActions.Instance.OnTurnChange -= HandleTurnChanged;
    }

    private void HandleTurnChanged (bool isPlayerTurn)
    {
        if (isPlayerTurn == true)
        {
            StartCardSelection();
        }
    }

    [Button]
    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            drawPile = new List<Card>(deck); // add reshuffle sound
            drawPileChanged.Invoke();
        }
        
        //cardSelectionOver.RemoveListener(DrawCard);
        int cardToDrawIndex = Random.Range(0, drawPile.Count);
        Card cardToDraw = drawPile[cardToDrawIndex];
        drawPile.Remove(cardToDraw);
        drawPileChanged.Invoke();

        currentCardInHand = cardToDraw;

        handText.text = cardToDraw.cardSO.cardDescription;
        activeCard.gameObject.SetActive(true);
    }
    
    public void PlayCard()
    {
        activeCard.gameObject.SetActive(false);

        cardPlayed.Invoke();
        GlobalActions.Instance.NotifyOnCardPlayed(currentCardInHand.cardSO.boundActionData);
    }

    public void GiveStartingCards()
    {
        deck = new List<Card>(startingDeck);
        drawPile = new List<Card>(deck);
        drawPileChanged.Invoke();
    }

    public void GainCard(Card cardToAdd)
    {
        deck.Add(cardToAdd);
    }

    public void StartCardSelection()
    {
        isCardSelectionInProgress = true;

        cardSelectionScreen.SetActive(true);

        for (int i = 0; i < numCardsToSelect; i++)
        {
            int randomCardIndex = Random.Range(0, allPlayableCards.Count);
            Card card = allPlayableCards[randomCardIndex];

            cardTextsForSelectionEvent[i].text = card.cardSO.cardDescription;
            cardCostTextsForSelectionEvent[i].text = card.cardSO.cost.ToString();

            selectionEventCards.Add(card);
        }
    }

    public void CardSelected(int cardChosenIndex)
    {
        Card chosenCard = selectionEventCards[cardChosenIndex];

        if (chosenCard.cardSO.cost > economyManager.currencyOwned) { return; }

        foreach (Card card in selectionEventCards)
        {
            card.gameObject.SetActive(false);
        }

        isCardSelectionInProgress = false;
        //cardSelectionOver.Invoke();
        economyManager.AddCurrency(-chosenCard.cardSO.cost);
        GainCard(chosenCard);
        ClearSelectionActionScreen();
        DrawCard();
    }

    public void SkipCardSelection()
    {
        ClearSelectionActionScreen();
    }
    private void ClearSelectionActionScreen()
    {
        selectionEventCards.Clear();
        cardSelectionScreen.SetActive(false);
        isCardSelectionInProgress = false; // zapytać Maćka czy da się to poprawić (na pewno się da xd) dla mnie to wygląda jakby powinno być eventem boolowym

        cardSelectionOver.Invoke();
    }
    public void StartCardRemoval()
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
        
        Card cardToRemove = deck[index];
        deck.Remove(cardToRemove);
        economyManager.AddCurrency(-removalCost);
        StartCardRemoval();

        if (drawPile.Contains(cardToRemove))
        {
            drawPile.Remove(cardToRemove);
            drawPileChanged.Invoke();
        }
    }
}
