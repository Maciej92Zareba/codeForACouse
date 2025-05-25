using UnityEngine;
using UnityEngine.Events;

public class RemoveCardsButtonHandler : MonoBehaviour

{
    [SerializeField] private GameObject cardSelectionScreen;
    [SerializeField] private GameObject removeCardsScreen;

    [SerializeField] private UnityEvent removeCardsScreenOn;
    [SerializeField] private UnityEvent cardSelectionScreenOn;

    public void ChangeBetweenScreens()
    {
        if (cardSelectionScreen.activeSelf)
        {
            removeCardsScreen.SetActive(true);
            cardSelectionScreen.SetActive(false);
            removeCardsScreenOn.Invoke();
        }
        else if (removeCardsScreen.activeSelf)
        {
            removeCardsScreen.SetActive(false);
            cardSelectionScreen.SetActive(true);
            cardSelectionScreenOn.Invoke();
        }
    }
}
