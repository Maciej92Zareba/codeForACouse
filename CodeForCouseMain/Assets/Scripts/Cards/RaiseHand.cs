using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaiseHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("CardRaiseAnimationSettings")]
    [SerializeField] float targetPosYOnHover = -280f;
    [SerializeField] float targetRotXOnHover = 0f;
    [SerializeField] private float cardRaiseTime = 0.2f;
    private float defaultPosY = -400f;
    private float defaultRotX = 30;

    [SerializeField] RectTransform rectTransform;

    private void Awake()
    {     
        defaultPosY = rectTransform.anchoredPosition.y;
        defaultRotX = rectTransform.rotation.eulerAngles.x;
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = new Vector2(0, defaultPosY);
        rectTransform.rotation = Quaternion.Euler(defaultRotX, 0, 0);
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
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(startPos, defaultPosY, tElapsed / cardRaiseTime));
            rectTransform.rotation = Quaternion.Euler(Mathf.Lerp(startRot, defaultRotX, tElapsed / cardRaiseTime), 0, 0);
            tElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
