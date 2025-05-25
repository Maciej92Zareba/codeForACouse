using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaiseHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("CardRaiseAnimationSettings")]
    [SerializeField] float targetPosYOnHover = -280f;
    [SerializeField] float targetRotXOnHover = 0f;
    [SerializeField] private float cardRaiseTime = 0.2f;

    [SerializeField] Vector2 inHandPos = new Vector3(0, -400);
    [SerializeField] Quaternion inHandRot = Quaternion.Euler(30, 0, 0);

    [SerializeField] Vector2 deckPos = new Vector3(0, -650);
    [SerializeField] Quaternion deckRot = Quaternion.Euler(45, 0, 0);
    [SerializeField] float cardDrawTime = 1f;

    [SerializeField] RectTransform rectTransform;

    private void OnEnable()
    {
        StartCoroutine(DrawCardRoutine());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(OnHoverRoutine());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(OnHoverExitRoutine());
    }

    private IEnumerator OnHoverRoutine()
    {

        float startPos = rectTransform.anchoredPosition.y;
        float startRot = rectTransform.rotation.eulerAngles.x;

        float tElapsed = 0f;
        while (cardRaiseTime > tElapsed)
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(startPos, targetPosYOnHover, tElapsed / cardRaiseTime));
            rectTransform.rotation = Quaternion.Euler(Mathf.Lerp(startRot, targetRotXOnHover, tElapsed / cardRaiseTime), 0, 0);
            tElapsed += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator OnHoverExitRoutine()
    {
        float startPos = rectTransform.anchoredPosition.y;
        float startRot = rectTransform.rotation.eulerAngles.x;

        float tElapsed = 0f;
        while (cardRaiseTime > tElapsed)
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(startPos, inHandPos.y, tElapsed / cardRaiseTime));
            rectTransform.rotation = Quaternion.Euler(Mathf.Lerp(startRot, inHandRot.x, tElapsed / cardRaiseTime), 0, 0);
            tElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DrawCardRoutine()
    {
        float tElapsed = 0f;
        while (cardDrawTime > tElapsed)
        {
            rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(deckPos.x, inHandPos.x, tElapsed / cardDrawTime), Mathf.Lerp(deckPos.y, inHandPos.y, tElapsed / cardDrawTime));
            rectTransform.rotation = Quaternion.Euler(Mathf.Lerp(deckRot.x, inHandPos.x, tElapsed / cardDrawTime), 0, 0);
            tElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
