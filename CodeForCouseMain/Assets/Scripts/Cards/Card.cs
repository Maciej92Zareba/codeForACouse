using System.Collections;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;

    [SerializeField] private float cardRiseTime = 0.2f;
    [SerializeField] private float yRiseAmount = 5f;
    [SerializeField] private float zRiseAmount = 5f;

    DeckManager deckManager;
    GridController gridController;

    private float startYPos;
    private float startZPos;

    private void Awake()
    {
        startYPos = transform.position.y;
        startZPos = transform.position.z;
    }
    public void Init(DeckManager dm, GridController gc)
    {
        deckManager = dm;
        gridController = gc;
    }

    private void OnMouseDown()
    {
        //playerInputController.PerformCardAction()
        // gridController.UpdateValidGridsToMove(); // update with Maciej
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        StartCoroutine(UpdatePositionCourutine());        
    }

    private void OnMouseExit()
    {
        StartCoroutine(ReturnToStartPositionRoutine());
    }

    private IEnumerator UpdatePositionCourutine()
    {
        float newYPos = startYPos + yRiseAmount;
        float newZPos = startZPos + zRiseAmount;
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;
        float elapsedTime = 0f;

        while (elapsedTime < cardRiseTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(currentYPos, newYPos, elapsedTime/cardRiseTime), Mathf.Lerp(currentZPos, newZPos, elapsedTime / cardRiseTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ReturnToStartPositionRoutine()
    {
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;
        float elapsedTime = 0f;

        while (elapsedTime < cardRiseTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(currentYPos, startYPos, elapsedTime / cardRiseTime), Mathf.Lerp(currentZPos, startZPos, elapsedTime / cardRiseTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}