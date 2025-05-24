using System.Collections;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{
    [SerializeField] public CardSO cardSO;

    [Header("CardRaiseAnimationSettings")]
    [SerializeField] float targetPosYOnHover = -280f;
    [SerializeField] private float cardRaiseTime = 0.2f;

    DeckManager deckManager;
    GridController gridController;

    public void Init(DeckManager dm, GridController gc)
    {
        deckManager = dm;
        gridController = gc;
    }

    private void OnMouseEnter()
    {
        StartCoroutine(OnHoverRoutine());
    }

    private IEnumerator OnHoverRoutine()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float startPos = rectTransform.anchoredPosition3D.x;
        Vector3 targetPos = new Vector3(0, targetPosYOnHover, 0);

        float tElapsed = 0f;
        while (cardRaiseTime <= tElapsed)
        {
            rectTransform.anchoredPosition3D = new Vector3(0, Mathf.Lerp(startPos, targetPosYOnHover, cardRaiseTime / tElapsed), 0);
            tElapsed += Time.deltaTime;
            yield return null;
        }
    }

    //[SerializeField] private float cardRiseTime = 0.2f;
    //[SerializeField] private float yRiseAmount = 5f;
    //[SerializeField] private float yRiseAmountDuringCardSelection = 5f;
    //[SerializeField] private float zRiseAmount = 5f;

    //DeckManager deckManager;
    //GridController gridController;

    //private float startYPos;
    //private float startZPos;

    //private void Awake()
    //{
    //    startYPos = transform.position.y;
    //    startZPos = transform.position.z;
    //}
    //public void Init(DeckManager dm, GridController gc)
    //{
    //    deckManager = dm;
    //    gridController = gc;
    //}

    //private void OnMouseDown()
    //{
    //    if (!deckManager.isCardSelectionEventActive)
    //    {
    //        //gridController.UpdateValidGridsToMove(); // update with Maciej
    //        Destroy(gameObject);        
    //    }
    //    else
    //    {
    //        deckManager.GainCard(this);
    //        gameObject.SetActive(false);
    //        deckManager.CardSelected();
    //    }
    //}

    //private void OnMouseEnter()
    //{
    //    if (deckManager.isCardSelectionEventActive)
    //    {
    //        StartCoroutine(UpdatePositionDuringCardSelectionRourutine());
    //    }
    //    else
    //    {
    //        StartCoroutine(UpdatePositionRourutine());
    //    }
    //}

    //private void OnMouseExit()
    //{
    //        StartCoroutine(ReturnToStartPositionRoutine());
    //}

    //private IEnumerator UpdatePositionRourutine()
    //{
    //    float newYPos = startYPos + yRiseAmount;
    //    float newZPos = startZPos + zRiseAmount;
    //    float currentYPos = transform.position.y;
    //    float currentZPos = transform.position.z;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < cardRiseTime)
    //    {
    //        transform.position = new Vector3(transform.position.x, Mathf.Lerp(currentYPos, newYPos, elapsedTime/cardRiseTime), Mathf.Lerp(currentZPos, newZPos, elapsedTime / cardRiseTime));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //}    private IEnumerator UpdatePositionDuringCardSelectionRourutine()
    //{
    //    float newYPos = startYPos + yRiseAmountDuringCardSelection;
    //    float currentYPos = transform.position.y;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < cardRiseTime)
    //    {
    //        transform.position = new Vector3(transform.position.x, Mathf.Lerp(currentYPos, newYPos, elapsedTime/cardRiseTime), transform.position.z);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    //private IEnumerator ReturnToStartPositionRoutine()
    //{
    //    float currentYPos = transform.position.y;
    //    float currentZPos = transform.position.z;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < cardRiseTime)
    //    {
    //        transform.position = new Vector3(transform.position.x, Mathf.Lerp(currentYPos, startYPos, elapsedTime / cardRiseTime), Mathf.Lerp(currentZPos, startZPos, elapsedTime / cardRiseTime));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}